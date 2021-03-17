using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Components;
using UserRegistration.Models;
using Newtonsoft.Json;
using System.Linq;
using UserRegistration.Components.Core;
using UserRegistration.Components.PluginSystem;

namespace AzureAD
{
    public class AzureAD : IPlugin
    {
        public string Name { get => nameof(AzureAD); }
        //public string ConnectionType { get => nameof(GraphAPIAuth); }
        private GraphServiceClient GraphServiceClient { get; set; }
        
        public AzureAD(Dictionary<object, object> Config)
        {
            var tenantId = Config["TenantId"].ToString();
            var clientId = Config["ClientId"].ToString();
            var clientSecret = Config["ClientSecret"].ToString();

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);

            GraphServiceClient = new GraphServiceClient(authProvider);
        }

        public async Task<List<string>> ReadUser()
        {
            var users = GraphServiceClient.Users.Request().GetAsync().Result;
            List<string> usersStrList = new List<string>();
            foreach (var item in users)
            {
                usersStrList.Add(item.DisplayName);
            }
            return usersStrList;
        }

        public async Task<List<string>> ReadGroups()
        {

            var groupsRequest = await GraphServiceClient.Groups.Request().GetAsync();

            List<string> groupsList = new List<string>();
            foreach (var group in groupsRequest)
            {
                groupsList.Add(group.DisplayName);
            }

            return groupsList;
        }
        
        public async Task Save(UserDestinationModel userToSave)
        {
            var user = new User
            {
                AccountEnabled = true,
                DisplayName = userToSave.FullName,
                MailNickname = userToSave.Login,
                UserPrincipalName = $"{userToSave.Login}@klokd2gmail.onmicrosoft.com",
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true
                }
            };

            await GraphServiceClient.Users
                .Request()
                .AddAsync(user);

            foreach (var group in userToSave.Groups)
            {
                var groupObj = await Helpers.GetGroupByName(group, GraphServiceClient);
                var userObj = await Helpers.GetUserByName(user.DisplayName, GraphServiceClient);
                await GraphServiceClient.Groups[groupObj.Id].Members.References
                    .Request()
                    .AddAsync(userObj);
            }
        }

        private class Helpers
        {
            public static async Task<Group> GetGroupByName(string groupName, GraphServiceClient graphServiceClient)
            {
                var targetGroupCollection = await graphServiceClient.Groups.Request()
                                            .Filter($"startsWith(displayName,'{groupName}')")
                                            .Select("displayName,id")
                                            .GetAsync();

                var targetGroup = targetGroupCollection.ToList().Where(g => g.DisplayName == groupName).FirstOrDefault();

                if (targetGroup != null)
                {
                    return targetGroup;
                }
                else
                {
                    throw new Exception($"{nameof(AzureAD)}.{nameof(GetGroupByName)} there is no group with name: {groupName}");
                }
            }

            public static async Task<User> GetUserByName(string userName, GraphServiceClient graphServiceClient)
            {
                var targetUserCollection = await graphServiceClient.Users.Request()
                                            .Filter($"startsWith(displayName,'{userName}')")
                                            .Select("displayName,id")
                                            .GetAsync();

                var targetUser = targetUserCollection.ToList().Where(g => g.DisplayName == userName).FirstOrDefault();

                if (targetUser != null)
                {
                    return targetUser;
                }
                else
                {
                    throw new Exception($"AzureAD.{nameof(GetUserByName)} there is no group with name: {userName}");
                }
                    
            }

            //public static async Task<Domain> GetDomain(string userName)
            //{
              
            //}
        }
    }
    
}
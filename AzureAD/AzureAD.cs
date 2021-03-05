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

namespace AzureAD
{
    public class AzureADConfig
    {

    }
    public class AzureAD : IService<AzureADConfig>
    {
        public async Task<List<string>> ReadUser()
        {
            var graphClient = Helpers.GetGraphClient();
            var users = graphClient.Users.Request().GetAsync().Result;
            List<string> usersStrList = new List<string>();
            foreach (var item in users)
            {
                usersStrList.Add(item.DisplayName);
            }
            return usersStrList;
        }

        public async Task<List<string>> ReadGroups()
        {
            var graphClient = Helpers.GetGraphClient();

            var groupsRequest = await graphClient.Groups.Request().GetAsync();

            List<string> groupsList = new List<string>();
            foreach (var group in groupsRequest)
            {
                groupsList.Add(group.DisplayName);
            }

            return groupsList;
        }
        

        public async Task Save(UserDestinationModel userToSave)
        {

            var graphClient = Helpers.GetGraphClient();
            var user = new User
            {
                AccountEnabled = true,
                DisplayName = userToSave.FullName,
                MailNickname = userToSave.Login,
                UserPrincipalName = $"{userToSave.Login}@klokd2gmail.onmicrosoft.com",
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = "newWeakPassword1"
                }
            };

            await graphClient.Users
                .Request()
                .AddAsync(user);

            foreach (var group in userToSave.Groups)
            {
                var groupObj = await Helpers.GetGroupByName(group);
                var userObj = await Helpers.GetUserByName(user.DisplayName);
                await graphClient.Groups[groupObj.Id].Members.References
                    .Request()
                    .AddAsync(userObj);
            }
        }

        private class Helpers
        {
            public static GraphServiceClient GetGraphClient()
            {
                var tenantId = "fd185bac-d7ba-412d-b46e-554186110ed4";
                var clientId = "d7424b64-e5a2-428e-b347-aaca025977b8";
                var clientSecret = "~RJy12y_RyQV18-XHhnFLR_ZSj.0x8z414";
                //092e6d06-58f2-4f26-8850-040eeabe653a

                IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(clientId)
                    .WithTenantId(tenantId)
                    .WithClientSecret(clientSecret)
                    .Build();

                ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
                return new GraphServiceClient(authProvider);
            }

            public static async Task<Group> GetGroupByName(string groupName)
            {
                var graphClient = GetGraphClient();

                var targetGroupCollection = await graphClient.Groups.Request()
                                            .Filter($"startsWith(displayName,'{groupName}')")
                                            .Select("displayName,id")
                                            .GetAsync();

                var targetGroup = targetGroupCollection.ToList().Where(g => g.DisplayName == groupName).FirstOrDefault();

                if (targetGroup != null)
                    return targetGroup;
                else
                    return null;
            }

            public static async Task<User> GetUserByName(string userName)
            {
                var graphClient = GetGraphClient();

                var targetUserCollection = await graphClient.Users.Request()
                                            .Filter($"startsWith(displayName,'{userName}')")
                                            .Select("displayName,id")
                                            .GetAsync();

                var targetUser = targetUserCollection.ToList().Where(g => g.DisplayName == userName).FirstOrDefault();

                if (targetUser != null)
                    return targetUser;
                else
                    return null;
            }

            //public static async Task<Domain> GetDomain(string userName)
            //{

            //}
        }
    }
    
}
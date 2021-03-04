using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Models;

namespace AzureAD
{
    public class AzureAD
    {
        private List<UserDestinationModel> UserDestinationCollection { get; set; }
        public async Task Read(UserDestinationModel userToSave)
        {
            var tenantId = "fd185bac-d7ba-412d-b46e-554186110ed4";
            var clientId = "d7424b64-e5a2-428e-b347-aaca025977b8";
            var clientSecret = ".~veKe4TI7V5HbDk5ydbz.c2skF~~AEwW5";
            //a23aec59-451c-4d84-8946-edda2b502138

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var users = graphClient.Users.Request().GetAsync().Result;
            List<string> usersStr = new List<string>();
            foreach (var item in users)
            {
                usersStr.Add(item.DisplayName);
            }

            if (Syncer.CompareUser(usersStr, userToSave.FullName) == false)
            {
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
                // TODO: UserPhotoAdd
                //using var stream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("C:\\Users\\ICS\\Pictures\\test.png"));

                //await graphClient.Me.Photo.Content
                //    .Request()
                //    .PutAsync(stream);
                await graphClient.Users
                    .Request()
                    .AddAsync(user);
            }
            else
            {
                Console.WriteLine("AzureAD: User already exists");
            }
        }
    }
}
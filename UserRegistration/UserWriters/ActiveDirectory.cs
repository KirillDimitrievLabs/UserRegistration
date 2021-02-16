using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserRegistration.UserWriters
{
    class ActiveDirectory : IUserWriter
    {
        public async void UserAdd(string username, string fullname, string password)
        {
            var tenantId = "fd185bac-d7ba-412d-b46e-554186110ed4";
            var clientId = "7e9c1504-edcb-4103-8e30-067e0e89e1d6";
            var clientSecret = "1dD3nr_~A7gdIp~xyCgN_Axj4z.l01eCzX";

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var user = new User
            {
                AccountEnabled = true,
                DisplayName = fullname,
                MailNickname = username,
                UserPrincipalName = $"{username}@klokd2gmail.onmicrosoft.com",
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = password
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
    }
}

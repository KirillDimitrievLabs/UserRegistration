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
            var clientId = "4caba94f-559f-4ea1-8c4c-9b595be709c3";
            var clientSecret = "~i7o2072D~IlC4hssi-tsqJ-XL~pd~dr~5";

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

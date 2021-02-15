using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YouTrackSharp;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System.Text;
using NGitLab;

namespace UserRegistration
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("username: ");
            String username = Console.ReadLine();

            Console.WriteLine("fullname: ");
            string fullname = Console.ReadLine();

            Console.WriteLine("email: ");
            string email = Console.ReadLine();

            Console.WriteLine("jabber: ");
            string jabber = Console.ReadLine();

            Console.WriteLine("password: ");
            string password = Console.ReadLine();

            Console.WriteLine("groupName: ");
            string groupName = Console.ReadLine();


            //YouTrackUserReg(username, fullname, email, jabber, password, groupName);
            //GitlabUserReg("", "", ""/* fullname,username, email*/);
            AzureUserReg(username, fullname, password);
            Console.ReadKey();
        }
        
        private async static void YouTrackUserReg(string username, string fullname, string email, string jabber, string password, string groupName)
        {
            var connection = new BearerTokenConnection("https://gitlab.com/api/v4/projects/24364874", "perm:cm9vdA==.NDYtMw==.91ZCenFwHUxaufYda8tsSZotjHcyJ9");
            var userManagementService = connection.CreateUserManagementService();
            await userManagementService.CreateUser(username, fullname, email, jabber, password);
            await userManagementService.AddUserToGroup(username, groupName);
        }

        private async static void GitLabUserReg(string fullname, string username, string email )
        {
            var client = GitLabClient.Connect("https://gitlab.com/testgroup528/test", "your_private_token");
        }

        private async static void AzureUserReg(string username, string fullname, string password)
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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Models;
using YouTrackSharp;
using System.Net.Http;
using UserRegistration.Components;

namespace YouTrack
{
    public class YouTrack : IService
    {
        private const string password = "Password1";
        private const string url = "https://apitesting.myjetbrains.com/youtrack/";
        private const string bearerToken = "perm:cm9vdA==.NDYtMA==.Z0V1zuAmnAcJhKXBAHgn0BHBbQyDYp";
        public static BearerTokenConnection Connection = new BearerTokenConnection(url, bearerToken);

        public async Task Read(UserDestinationModel userToSave)
        {
            var exsistingUsers = await Connection.CreateUserManagementService().GetUsers();
            List<string> exsistingUserStr = new List<string>();
            foreach (var exsistingUser in exsistingUsers)
            {
                exsistingUserStr.Add(exsistingUser.Username);
            }
            if(Syncer.CompareUser(exsistingUserStr, userToSave.Login)==false)
            {
                await Save(userToSave);
                Console.WriteLine($"{userToSave.Login} added");
            }
            else
            {
                Console.WriteLine("YouTrack: User already exsist");
            }
            
        }
        public async Task Save(UserDestinationModel userToSave)
        {
            await Connection.CreateUserManagementService().CreateUser(userToSave.Login,userToSave.FullName, userToSave.Email, "",password);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Models;
using YouTrackSharp;
using YouTrackSharp.Management;
using System.Net.Http;
using UserRegistration.Components;
using Newtonsoft.Json;

namespace YouTrack
{
    public class YouTrackConfig
    {
        public string BearerToken { get; set; }
        public string Url { get; set; }
    }


    public class YouTrack : IService<YouTrackConfig>
    {
        private const string url = "https://apitesting.myjetbrains.com/youtrack/";
        private const string bearerToken = "perm:cm9vdA==.NDYtMA==.Z0V1zuAmnAcJhKXBAHgn0BHBbQyDYp";
        public static BearerTokenConnection Connection = new BearerTokenConnection(url, bearerToken);

        public YouTrack(YouTrackConfig config)
        {

        }

        public async Task<List<string>> ReadUser()
        {
            var exsistingUsers = await Connection.CreateUserManagementService().GetUsers();
            List<string> exsistingUserStrList = new List<string>();

            foreach (var exsistingUser in exsistingUsers)
            {
                exsistingUserStrList.Add(exsistingUser.FullName);
            }
            return exsistingUserStrList;
        }

        public async Task<List<string>> ReadGroups()
        {
            var client = await Connection.GetAuthenticatedHttpClient();

            var response = await client.GetAsync($"rest/admin/group");

            var groupJson = JsonConvert.DeserializeObject<List<Group>>(
                await response.Content.ReadAsStringAsync());

            List<string> exsistingUserStr = new List<string>();
            foreach (var groupName in groupJson)
            {
                exsistingUserStr.Add(groupName.Name);
            }

            return exsistingUserStr;
        }

        public async Task Save(UserDestinationModel userToSave)
        {
            await Connection
                .CreateUserManagementService()
                .CreateUser(userToSave.Login, userToSave.FullName, userToSave.Email, "", "Password1");

            foreach (var userGroup in userToSave.Groups)
            {
                await Connection
                    .CreateUserManagementService()
                    .AddUserToGroup(userToSave.Login, userGroup);
            }
        }
    }
}
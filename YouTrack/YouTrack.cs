using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Models;
using YouTrackSharp;
using YouTrackSharp.Management;
using System.Net.Http;
using UserRegistration.Components;
using Newtonsoft.Json;
using UserRegistration.Components.Core;
using UserRegistration;
using UserRegistration.Components.PluginSystem;

namespace YouTrack
{
    public class YouTrack : IPlugin
    {
        //public string ConnectionType { get => nameof(TokenAuth); }
        public string Name { get => nameof(YouTrack); }
        private BearerTokenConnection Connection { get; set; }

        public YouTrack(Dictionary<object, object> Config)
        {
            Connection = new BearerTokenConnection(Config["Url"].ToString(), Config["Token"].ToString());
        }

        public async Task<List<string>> ReadUsers()
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

        public Task Update(UserDestinationModel userToUpdate)
        {
            //await Connection.CreateUserManagementService()
            //      .UpdateUser(userToUpdate.Login,userToUpdate.FullName, userToUpdate.Email);
            return Task.Run(() => Console.WriteLine($"{nameof(YouTrack)}Can't update user due to API restrictions"));
        }

        public Task Delete(UserDestinationModel userToDelete)
        {
            //rawait Connection.GetAuthenticatedHttpClient().Result.DeleteAsync($"/users/{userToDelete.Login}");
            return Task.Run(() => Console.WriteLine($"{nameof(YouTrack)}Can't delete user due to API restrictions"));
        }
    }
}
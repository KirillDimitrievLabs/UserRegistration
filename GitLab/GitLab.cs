using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Models;
using GitLabApiClient;
using GitLabApiClient.Models.Users.Responses;
using GitLabApiClient.Models.Users.Requests;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Groups.Responses;
using System.Linq;
using System;
using UserRegistration.Components.PluginSystem;

namespace GitLab
{
    public class GitLab : IPlugin
    {
        public string Name { get => nameof(GitLab); }
        //public string ConnectionType { get => nameof(TokenAuth); }
        private GitLabClient GitLabClient { get; set; }

        public GitLab(Dictionary<object, object> Config)
        {
            GitLabClient = new GitLabClient(Config["Url"].ToString(), Config["Token"].ToString());
        }

        public async Task<List<string>> ReadGroups()
        {
            var groupsRequest = await GitLabClient.Groups.GetAsync();
            List<string> groupsList = new List<string>();
            foreach (var group in groupsRequest)
            {
                groupsList.Add(group.FullName);
            }
            return groupsList;
        }

        public async Task<List<string>> ReadUser()
        {
            var userRequest = await GitLabClient.Users.GetAsync();
            List<string> groupsList = new List<string>();
            foreach (var user in userRequest)
            {
                groupsList.Add(user.Name);
            }
            return groupsList;
        }

        public async Task Save(UserDestinationModel userToSave)
        {
            await GitLabClient.Users.CreateAsync(new CreateUserRequest(userToSave.FullName, userToSave.Login, userToSave.Email));
            foreach (var group in userToSave.Groups)
            {
                Group groupRequest = await Helpers.GetGroupByName(group, GitLabClient);
                GroupId groupId = groupRequest.Id;

                User userRequest = await Helpers.GetUserByName(userToSave.FullName, GitLabClient);
                UserId userId = userRequest.Id;

                var request = await GitLabClient.Groups.AddMemberAsync(groupId, new AddGroupMemberRequest(GitLabApiClient.Models.AccessLevel.Guest, userId));
            }
        }

        private class Helpers
        {
            public static async Task<Group> GetGroupByName(string groupName, GitLabClient gitLabClient)
            {
                var request = await gitLabClient.Groups.SearchAsync(groupName);

                var targetGroup = request.ToList().Where(g => g.FullName == groupName).FirstOrDefault();

                if (targetGroup != null)
                {
                    return targetGroup;
                }
                else
                {
                    throw new Exception($"{nameof(GitLab)}.{nameof(GetGroupByName)} there is no group with name: {groupName}");
                }
                    
            }

            public static async Task<User> GetUserByName(string userName, GitLabClient gitLabClient)
            {
                var request = await gitLabClient.Users.GetByFilterAsync(userName);

                var targetGroup = request.ToList().Where(g => g.Name == userName).FirstOrDefault();

                if (targetGroup != null)
                {
                    return targetGroup;
                }
                else
                {
                    throw new Exception($"{nameof(GitLab)}.{nameof(GetUserByName)} there is no User with name: {userName}");
                }
                    
            }
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Components;
using UserRegistration.Models;
using GitLabApiClient;
using GitLabApiClient.Models.Users.Responses;
using GitLabApiClient.Models.Users.Requests;
using GitLabApiClient.Models.Groups.Requests;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Groups.Responses;
using System.Linq;

namespace GitLab
{
    public class GitLab : IService
    {
        public List<UserDestinationModel> UserDestinationCollection { get; set; }
        private static readonly GitLabClient client = new GitLabClient("https://gitlab.example.com", "your_private_token");
        public async Task<List<string>> ReadGroups()
        {
            var groupsRequest = await client.Groups.GetAsync();
            List<string> groupsList = new List<string>();
            foreach (var group in groupsRequest)
            {
                groupsList.Add(group.FullName);
            }
            return groupsList;
        }

        public async Task<List<string>> ReadUser()
        {
            var userRequest = await client.Users.GetAsync();
            List<string> groupsList = new List<string>();
            foreach (var user in userRequest)
            {
                groupsList.Add(user.Name);
            }
            return groupsList;
        }

        public async Task Save(UserDestinationModel userToSave)
        {
            await client.Users.CreateAsync(new CreateUserRequest(userToSave.FullName, userToSave.Login, userToSave.Email));
            foreach (var group in userToSave.Groups)
            {
                Group groupRequest = await Helpers.GetGroupByName(group);
                GroupId groupId = groupRequest.Id;

                User userRequest = await Helpers.GetUserByName(userToSave.FullName);
                UserId userId = userRequest.Id;

                var re = await client.Groups.AddMemberAsync(groupId, new AddGroupMemberRequest(GitLabApiClient.Models.AccessLevel.Guest, userId));
            }
        }

        private class Helpers
        {
            public static async Task<Group> GetGroupByName(string groupName)
            {
                var request = await client.Groups.SearchAsync(groupName);

                var targetGroup = request.ToList().Where(g => g.FullName == groupName).FirstOrDefault();

                if (targetGroup != null)
                    return targetGroup;
                else
                    return null;
            }

            public static async Task<User> GetUserByName(string userName)
            {
                var request = await client.Users.GetByFilterAsync(userName);

                var targetGroup = request.ToList().Where(g => g.Name == userName).FirstOrDefault();

                if (targetGroup != null)
                    return targetGroup;
                else
                    return null;
            }
        }
    }
}

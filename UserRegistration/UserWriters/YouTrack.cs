using YouTrackSharp;

namespace UserRegistration.UserWriters
{
    class YouTrack : IUserWriter
    {
        public async void UserAdd(string username, string fullname, string email, string password, string groupName)
        {
            var connection = new BearerTokenConnection("https://gitlab.com/api/v4/projects/24364874", "perm:cm9vdA==.NDYtMw==.91ZCenFwHUxaufYda8tsSZotjHcyJ9");
            var userManagementService = connection.CreateUserManagementService();
            await userManagementService.CreateUser(username, fullname, email, "", password);
            await userManagementService.AddUserToGroup(username, groupName);
            // TODO: UserPhotoAdd
        }
    }
}

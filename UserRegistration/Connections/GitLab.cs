using NGitLab;

namespace UserRegistration.UserWriters
{
    class GitLab : IUserWriter
    {
        public void UserAdd(string fullname, string username, string email)
        {
            var client = GitLabClient.Connect("https://gitlab.com/testgroup528/test", "your_private_token");
        }
    }
}

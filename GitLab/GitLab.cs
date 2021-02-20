using System;
using UserRegistration.Models;

namespace GitLab
{
    public class GitLab
    {
        public UserDestinationModel UserDestination { get; set; }
        public void GitlabService()
        {
            UserDestination = Syncer.GetUserDestination();
            Console.WriteLine($"{nameof(GitlabService)} has been loaded");
            Console.WriteLine("UserLogin: " + UserDestination.Login);
        }
    }
}

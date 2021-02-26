using System;
using System.Collections.Generic;
using UserRegistration.Models;

namespace GitLab
{
    public class GitLab
    {
        public List<UserDestinationModel> UserDestinationCollection { get; set; }
        public void gitlabservice()
        {
            UserDestinationCollection = Syncer.GetUserDestination();
            Console.WriteLine($"{nameof(gitlabservice)} has been loaded");
            foreach (var userDestination in UserDestinationCollection)
            {
                Console.WriteLine("UserLogin: " + userDestination.Login);
            }
        }
    }
}

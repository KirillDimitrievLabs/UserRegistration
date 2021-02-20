using System;
using UserRegistration.Models;

namespace YouTrack
{
    public class YouTrack
    {
        public UserDestinationModel UserDestination { get; set; }
        public void YoutrackService()
        {
            UserDestination = Syncer.GetUserDestination();
            Console.WriteLine($"{nameof(YoutrackService)} has been loaded");
            Console.WriteLine("UserLogin: " + UserDestination.Login);
            
        }
    }
}

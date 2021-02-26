using System;
using System.Collections.Generic;
using UserRegistration.Models;

namespace YouTrack
{
    public class YouTrack
    {
        public List<UserDestinationModel> UserDestinationCollection { get; set; }
        public void youtrackservice()
        {
            UserDestinationCollection = Syncer.GetUserDestination();
            Console.WriteLine($"{nameof(youtrackservice)} has been loaded");
            foreach (var userDestination in UserDestinationCollection)
            {
                Console.WriteLine("UserLogin: " + userDestination.Fullname);
            }
        }
    }
}

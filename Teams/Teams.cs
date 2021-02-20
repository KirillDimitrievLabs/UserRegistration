using System;
using UserRegistration.Models;

namespace Teams
{
    public class Teams
    {
        public UserDestinationModel UserDestination { get; set; }
        public void TeamsService()
        {
            UserDestination = Syncer.GetUserDestination();
            Console.WriteLine($"{nameof(TeamsService)} has been loaded");
            Console.WriteLine("UserLogin: " + UserDestination.Login);
        }
    }
}

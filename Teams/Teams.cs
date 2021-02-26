using System;
using UserRegistration.Models;

namespace Teams
{
    public class Teams
    {
        public UserDestinationModel UserDestination { get; set; }
        public void teamsservice()
        {
            //UserDestination = Syncer.GetUserDestination();
            Console.WriteLine($"{nameof(teamsservice)} has been loaded");
            Console.WriteLine("UserLogin: " + UserDestination.Login);
        }
    }
}

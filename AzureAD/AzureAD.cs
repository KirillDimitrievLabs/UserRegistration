using System;
using UserRegistration.Models;

namespace AzureAD
{
    public class AzureAD
    {
        public UserDestinationModel UserDestination { get; set; }
        public void AzureadService()
        {
            UserDestination = Syncer.GetUserDestination();
            Console.WriteLine($"{nameof(AzureadService)} has been loaded");
            Console.WriteLine("UserLogin: " + UserDestination.Login);
        }
    }
}

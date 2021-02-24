using System;
using System.Collections.Generic;
using UserRegistration.Models;

namespace AzureAD
{
    public class AzureAD
    {
        public List<UserDestinationModel> UserDestinationCollection { get; set; }
        public void AzureadService()
        {
            UserDestinationCollection = Syncer.GetUserDestination();
            Console.WriteLine($"{nameof(AzureadService)} has been loaded");
            foreach (var userDestination in UserDestinationCollection)
            {
                Console.WriteLine("UserLogin: " + userDestination.Fullname);
            }
        }
    }
}

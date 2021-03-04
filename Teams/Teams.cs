using System;
using UserRegistration.Models;
using UserRegistration.Components;
using System.Threading.Tasks;

namespace Teams
{
    public class Teams : IService
    {
        public UserDestinationModel UserDestination { get; set; }

        public Task Read(UserDestinationModel userToSave)
        {
            throw new NotImplementedException();
        }

        public Task Save(UserDestinationModel userToSave)
        {
            //UserDestination = Syncer.GetUserDestination();
            Console.WriteLine($"{nameof(Save)} has been loaded");
            Console.WriteLine("UserLogin: " + UserDestination.Login);
            throw new NotImplementedException();
        }

    }
}

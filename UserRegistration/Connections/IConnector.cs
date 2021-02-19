using UserRegistration.Components;
using UserRegistration.Models;

namespace UserRegistration.Connections
{
    interface IConnector<T>
    {
        string login { get; set; }
        public void Read(string login);
        //public void Save(UserConverter userDestination);
    }
}

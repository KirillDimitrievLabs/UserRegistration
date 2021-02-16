using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using UserRegistration.Models;

namespace UserRegistration.Connections
{
    interface IConnector<T>
    {
        string login { get; set; }
        public void Read(string login);
        public void Save(UserDestinationModel userDestination);
    }
}

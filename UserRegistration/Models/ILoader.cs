using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistration.Models
{
    interface ILoader
    {
        public ConnectionModel Connection { get; set; }
        public UserSourceModel UserSource { get; set; }
        public Config Destination { get; set; }
        public void Read();
        public void Load(UserSourceModel userSource, UserDestinationModel userDestination, string appConfigPath);
    }
    interface IService
    {
        public void Create();
    }
}

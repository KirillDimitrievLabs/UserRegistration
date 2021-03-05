using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistration.Models
{
    interface ILoader
    {
        public void Load();
    }
    interface IService
    {
        public void Create();
    }
}

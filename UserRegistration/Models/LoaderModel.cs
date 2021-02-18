using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Components;

namespace UserRegistration.Models
{
    class LoaderModel
    {
        public UserSource UserSource { get; set; }
        public string Destination { get; set; }
    }
}

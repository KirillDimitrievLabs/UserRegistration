using System.Collections;
using System.Collections.Generic;

namespace UserRegistration.Models
{
    public class ConnectionModel
    {
        public bool Youtrack { get; set; } = false;
        public bool Gitlab { get; set; } = false;
        public bool Azuread { get; set; } = false;
        public bool Teams { get; set; } = false;
    }
}

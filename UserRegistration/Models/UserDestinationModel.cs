using System;

namespace UserRegistration.Models
{
    public class UserDestinationModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool Disabled { get; set; }
        public string UserName { get; set; }
        public string[] Groups { get; set; }
    }
}

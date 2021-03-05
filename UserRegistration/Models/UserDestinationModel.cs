using System.Collections.Generic;

namespace UserRegistration.Models
{
    public class UserDestinationModel : IUserModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Disabled { get; set; }
        public string Login { get; set; }
        public string Avatar { get; set; }
        public List<string> Groups { get; set; }
    }
}

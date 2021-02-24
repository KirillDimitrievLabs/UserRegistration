using System.Collections.Generic;

namespace UserRegistration.Models
{
    public class UserDestinationModel : IUserModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool Disabled { get; set; }
        public string Login { get; set; }
        public string Avatar { get; set; }
        public string[] Groups { get; set; }
        public Dictionary<string, dynamic> GetValues()
        {
            Dictionary<string, dynamic> valuesDictionary = new Dictionary<string, dynamic>
            {
                { nameof(Fullname), Fullname },
                { nameof(Email), Email },
                { nameof(Disabled), Disabled.ToString() },
                { nameof(Login), Login },
                { nameof(Avatar), Avatar },
                { nameof(Groups),Groups }
            };
            return valuesDictionary;
        }
    }
}

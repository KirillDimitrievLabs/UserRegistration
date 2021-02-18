using System.Collections.Generic;

namespace UserRegistration.Models
{
    public class UserSourceModel : IUserModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool Disabled { get; set; }
        public string Avatar { get; set; } // TODO: Изменить тип для Avatar
        public Dictionary<string, OrgStructure> Orgstructure { get; set; }
    }
    public class OrgStructure
    {
        public string Team { get; set; }
        public string Office { get; set; }
        public string Company { get; set; }

        public static string[] ConvertToStringArray(OrgStructure orgStructure)
        {
            string[] result = { orgStructure.Company, orgStructure.Office, orgStructure.Team };
            return result;
        }
    }
}

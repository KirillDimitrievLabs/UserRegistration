using System;
using UserRegistration.Models;

namespace UserRegistration.Components
{
    public class Convertor : UserDestinationModel
    {
        public Convertor()
        {
            userSource = Yaml<UserSourceModel>.YamlToModel(@"\InputData\UserSource.yaml");
            Fullname = userSource.Fullname;
            Login = userSource.Fullname.Replace(' ', '_');
            Avatar = userSource.Avatar;
            Disabled = userSource.Disabled;
            Email = userSource.Email;
            Groups = GroupFormater(userSource.Orgstructure["group"]);
        }
        static private string[] GroupFormater(OrgStructure groupSourceString)
        {
            groupSourceString.Company = string.Format($"@{nameof(groupSourceString.Company)} {groupSourceString.Company}");
            groupSourceString.Team = string.Format($"@{nameof(groupSourceString.Team)} {groupSourceString.Team}");
            groupSourceString.Office = string.Format($"@{nameof(groupSourceString.Office)} {groupSourceString.Office}");
            string[] result = OrgStructure.ConvertToStringArray(groupSourceString);
            return result;
        }
    }
}

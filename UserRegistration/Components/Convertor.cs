using System;
using UserRegistration.Models;

namespace UserRegistration.Components
{
    public class Convertor
    {
        public UserDestinationModel Convert(UserSource userSource, UserDestination userDestination)
        {
            userDestination.Fullname = userSource.Fullname;
            userDestination.Login = userSource.Fullname.Replace(' ', '_');
            userDestination.Avatar = userSource.Avatar;
            userDestination.Disabled = userSource.Disabled;
            userDestination.Email = userSource.Email;
            userDestination.Groups = GroupFormater(userSource.Orgstructure["group"]);
            return userDestination;
        }
        private string[] GroupFormater(OrgStructure groupSourceString)
        {
            groupSourceString.Company = string.Format($"@{nameof(groupSourceString.Company)} {groupSourceString.Company}");
            groupSourceString.Team = string.Format($"@{nameof(groupSourceString.Team)} {groupSourceString.Team}");
            groupSourceString.Office = string.Format($"@{nameof(groupSourceString.Office)} {groupSourceString.Office}");
            string[] result = OrgStructure.ConvertToStringArray(groupSourceString);
            return result;
        }
    }
}

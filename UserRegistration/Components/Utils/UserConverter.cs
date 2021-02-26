using System;
using System.Collections.Generic;
using UserRegistration.Models;

namespace UserRegistration.Components
{
    public static class UserConverter
    {
        public static List<UserDestinationModel> ToUserDestinationModel(List<UserSourceModel> userSourceCollection)
        {
            List<UserDestinationModel> userDestinationCollection = new List<UserDestinationModel>();
            foreach (var userSource in userSourceCollection)
            {
                UserDestinationModel userDestination = new UserDestinationModel 
                {
                    Fullname = userSource.Fullname,
                    Login = userSource.Fullname.Replace(' ', '_'),
                    Avatar = userSource.Avatar,
                    Disabled = userSource.Disabled,
                    Email = userSource.Email,
                    Groups = GroupFormater(userSource.Orgstructure)
                };
                userDestinationCollection.Add(userDestination);
            }
            return userDestinationCollection;
        }

        private static string[] GroupFormater(OrgStructure groupSourceString)
        {
            groupSourceString.Company = string.Format($"@{nameof(groupSourceString.Company)} {groupSourceString.Company}");
            groupSourceString.Team = string.Format($"@{nameof(groupSourceString.Team)} {groupSourceString.Team}");
            groupSourceString.Office = string.Format($"@{nameof(groupSourceString.Office)} {groupSourceString.Office}");
            string[] result = OrgStructure.ConvertToStringArray(groupSourceString);
            return result;
        }
    }
}
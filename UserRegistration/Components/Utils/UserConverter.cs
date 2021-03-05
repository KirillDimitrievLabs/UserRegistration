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
                    FullName = userSource.FullName,
                    Login = userSource.FullName.Replace(' ', '_'),
                    Avatar = userSource.Avatar,
                    Disabled = userSource.Disabled,
                    Email = userSource.Email,
                    Groups = GroupFormater(userSource.OrgStructure)
                };
                userDestinationCollection.Add(userDestination);
            }
            return userDestinationCollection;
        }

        private static string[] GroupFormater(OrgStructureModel groupSourceString)
        {
            var Company = $"@{nameof(groupSourceString.Company)} {groupSourceString.Company}";
            var Office = $"@{nameof(groupSourceString.Office)} {groupSourceString.Office}";
            var Team = $"@{nameof(groupSourceString.Team)} {groupSourceString.Team}";

            string[] result = { Company, Office, Team };
            return result;
        }
    }
}
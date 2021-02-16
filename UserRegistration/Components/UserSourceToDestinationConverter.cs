using System;
using System.IO;
using UserRegistration.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace UserRegistration.Components
{
    public class UserSourceToDestinationConverter : IUserSourceToDestinationConverter
    {
        public UserDestinationModel Convert()
        {
            UserDestinationModel userDestinationModel = new UserDestinationModel();
            userDestinationModel.FullName = YamlToUserSource().Fullname;
            userDestinationModel.UserName = YamlToUserSource().Fullname.Replace(' ','_');
            userDestinationModel.Avatar = YamlToUserSource().Avatar;
            userDestinationModel.Disabled = YamlToUserSource().Disabled;
            userDestinationModel.Email = YamlToUserSource().Email;
            userDestinationModel.Groups = GroupFormater(YamlToUserSource().Orgstructure["group"]);
            return userDestinationModel;
        }

        static private UserSourceModel YamlToUserSource()
        {
            string writePath = @"C:\Users\user\source\repos\UserRegistration\UserRegistration\InputData\UserSource.yaml";
            UserSourceModel userSource = new UserSourceModel();
            string yml = @"";
            using (StreamReader sr = new StreamReader(writePath))
            {
                yml = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder()
               .WithNamingConvention(UnderscoredNamingConvention.Instance)
               .Build();
            userSource = deserializer.Deserialize<UserSourceModel>(yml);
            return userSource;
        }

        static private string[] GroupFormater(OrgStructure groupSourceString)
        {
            groupSourceString.Company = String.Format($"@{nameof(groupSourceString.Company)} {groupSourceString.Company}");
            groupSourceString.Team = String.Format($"@{nameof(groupSourceString.Team)} {groupSourceString.Team}");
            groupSourceString.Office = String.Format($"@{nameof(groupSourceString.Office)} {groupSourceString.Office}");
            string[] result = OrgStructure.ConvertToStringArray(groupSourceString);
            return result;
        }
    }
}

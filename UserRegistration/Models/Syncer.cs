using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Components;

namespace UserRegistration.Models
{
    public static class Syncer
    {
        private static List<UserSourceModel> ReadUserSource()
        {
            List<UserSourceModel> userSourceModel = Yaml<List<UserSourceModel>>.YamlToModel(@"\Source\UserSource.yaml");
            return userSourceModel;
        }
        public static List<UserDestinationModel> GetUserDestination()
        {
            return UserConverter.ToUserDestinationModel(ReadUserSource());
        }
    }
}
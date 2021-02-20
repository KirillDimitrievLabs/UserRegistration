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
        private static UserSourceModel ReadUserSource()
        {
            UserSourceModel userSourceModel = Yaml<UserSourceModel>.YamlToModel(@"\Source\UserSource.yaml");
            return userSourceModel;
        }
        public static UserDestinationModel GetUserDestination()
        {
            UserDestinationModel userDestinationModel = new UserDestinationModel();
            return UserConverter.ToUserDestinationModel(ReadUserSource(), userDestinationModel);
        }
        // TODO: GetUserDestinationCollection
    }
}
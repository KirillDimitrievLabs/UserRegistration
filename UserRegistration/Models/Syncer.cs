using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Components;

namespace UserRegistration.Models
{
    static class Syncer
    {
        public static UserSourceModel ReadSource(string sourcePath)
        {
            UserSourceModel userSourceModel= new UserSourceModel();
            userSourceModel = Yaml<UserSourceModel>.YamlToModel(sourcePath);
            return userSourceModel;
        }
        public static UserDestinationModel ConvertToDestination(UserSourceModel userSource)
        {
            UserDestinationModel userDestinationModel = new UserDestinationModel();
            return UserConverter.ToUserDestinationModel(userSource, userDestinationModel);
        }
    }
}
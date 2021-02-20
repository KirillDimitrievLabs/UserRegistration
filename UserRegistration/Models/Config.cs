using System;
using System.IO;
using System.Reflection;
using UserRegistration.Components;

namespace UserRegistration.Models
{
    class Config
    {
        public string Connectioncode { get; set; }
        public void Write (ConnectionModel connectionModel)
        {
            Connectioncode = Yaml<Config>.YamlToModel(@"\Source\Config.yaml").Connectioncode;
            foreach (var item in Connectioncode.ToLower().Split(','))
            {
                switch (item)
                {
                    case "youtrack":
                        connectionModel.Youtrack = true;
                        break;
                    case "gitlab":
                        connectionModel.Gitlab = true;
                        break;
                    case "azuread":
                        connectionModel.Azuread = true;
                        break;
                    case "teams":
                        connectionModel.Teams = true;
                        break;
                    default:
                        break;
                }
            }
            Yaml<ConnectionModel>.ModelToYaml(connectionModel, "AppConfig.yaml");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Models;

namespace UserRegistration.Components
{
    class Config : ConfigModel
    {
        public Config()
        {
            Connectioncode = Yaml<ConfigModel>.YamlToModel(@"\InputData\Config.yaml").Connectioncode;
        }
        public void Write()
        {
            ConnectionModel connectionModel = new ConnectionModel();
            foreach (var item in Connectioncode)
            {
                switch (item)
                {
                    case '0':
                        connectionModel.YouTrack = true;
                        Console.WriteLine($"Сборка {nameof(connectionModel.YouTrack)} загружена");
                        break;
                    case '1':
                        connectionModel.Gitlab = true;
                        Console.WriteLine($"Сборка {nameof(connectionModel.Gitlab)} загружена");
                        break;
                    case '2':
                        connectionModel.AzureAD = true;
                        Console.WriteLine($"Сборка {nameof(connectionModel.AzureAD)} загружена");
                        break;
                    case '3':
                        connectionModel.Teams = true;
                        Console.WriteLine($"Сборка {nameof(connectionModel.Teams)} загружена");
                        break;
                    default:
                        break;
                }
            }
            Yaml<ConnectionModel>.ModelToYaml(connectionModel, "AppConfig.yaml");
        }
    }
}

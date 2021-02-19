using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Components;
using System.Reflection;
using System.Runtime.InteropServices;

namespace UserRegistration.Models
{
    public static class Loader
    {
        public static void Load(string appConfigPath)
        {
            Config config = new Config();
            ConnectionModel connection = ReadConfig(appConfigPath);
            config.Write(connection);
            Service.Create(connection);
        }

        private static ConnectionModel ReadConfig(string appConfigPath)
        {
            ConnectionModel connectionModel = Yaml<ConnectionModel>.YamlToModel(appConfigPath);
            return connectionModel;
        }
        public static class Service
        {
            public static void Create(ConnectionModel connectionModel)
            {
                var connections = connectionModel.GetType().GetProperties();
                foreach (var item in connections)
                {
                    if ((bool)item.GetValue(connectionModel) == true)
                    {
                        Console.WriteLine($"{item.Name} created");
                    }
                }
            }
        }
    }
}

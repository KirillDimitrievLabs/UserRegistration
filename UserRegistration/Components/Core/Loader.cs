using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Components;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;

namespace UserRegistration.Models
{
    public static class Loader
    {
        public static async Task Load()
        {
            await Service.Create(Config.ReadConfig());
        }

        public static class Service
        {
            public static async Task Create(Config connection)
            {
                foreach (var item in connection.ConnectionCode.Split(","))
                {
                    try
                    {
                        await Syncer.GetSyncer().CreateUser(LoadLibrary(item));
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine($"ERROR: {connection.ConnectionCode} not found");
                    }
                }
            }

            private static Assembly LoadLibrary(string libraryToLoad)
            {
                Assembly serviceDll = Assembly.LoadFrom(Directory.GetCurrentDirectory() + $@"\{libraryToLoad}.dll");
                return serviceDll;
            }
        }

        public class Config
        {
            public string ConnectionCode { get; set; }
            public static Config ReadConfig()
            {
                Config config = Yaml<Config>.YamlToModel(@"Config.yaml");
                return config;
            }
        }
    }
}
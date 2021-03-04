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
                    await LoadLibrary(item);
                }
            }

            public static async Task LoadLibrary(string libraryToLoad)
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(Directory.GetCurrentDirectory() + $@"\{libraryToLoad}.dll");

                    Type type = asm.GetType($"{asm.GetName().Name}.{asm.GetName().Name}");
                    object obj = asm.CreateInstance(type.ToString());
                    MethodInfo method = type.GetMethod("Read");
                    foreach (var user in Syncer.GetSyncer().GetConvertedUsers())
                    {
                        object[] userobj = { user };
                        await (Task)method.Invoke(obj,userobj);
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: {libraryToLoad}.dll not found");
                    Console.ResetColor();
                }
                catch (TargetParameterCountException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Invalid parameters");
                    Console.ResetColor();
                }
                catch (NullReferenceException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Method not found");
                    Console.ResetColor();
                }
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
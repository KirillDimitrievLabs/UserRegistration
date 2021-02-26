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
        public static void Load()
        {
            Service.Create(Config.ReadConfig());
        }

        public static class Service
        {
            public static void Create(Config connection)
            {
                foreach (var item in connection.ConnectionCode.Split(","))
                {
                    try
                    {
                        LoadLibrary(item);
                        Console.WriteLine($"{item} created\n");
                    }
                    catch (FileNotFoundException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Dll not found");
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

            public static void LoadLibrary(string libraryToLoad)
            {
                Assembly asm = Assembly.LoadFrom(Directory.GetCurrentDirectory() + $@"\{libraryToLoad}.dll");
                Type type = asm.GetType($"{asm.GetName().Name}.{asm.GetName().Name}");
                Object obj = asm.CreateInstance(type.ToString());
                MethodInfo method = type.GetMethod(libraryToLoad.ToLower() + "service");
                method.Invoke(obj, Array.Empty<object>());
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
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
                PropertyInfo[] connections = connectionModel.GetType().GetProperties();
                foreach (var item in connections)
                {
                    if ((bool)item.GetValue(connectionModel) == true)
                    {
                        try
                        {
                            LoadLibrary(item.Name);
                            Console.WriteLine($"{item.Name} created\n");
                        }
                        catch (FileNotFoundException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Dll not found");
                            Console.ResetColor();
                        }
                        catch (NullReferenceException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Method not found");
                            Console.ResetColor();
                        }
                        catch (TargetParameterCountException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: Invalid parameters");
                            Console.ResetColor();
                        }

                    }
                }
            }
            public static void LoadLibrary(string libraryToLoad)
            {
                Assembly asm = Assembly.LoadFrom($"{libraryToLoad}.ll");
                Type[] type = asm.GetTypes();
                Object obj = asm.CreateInstance(type[0].ToString());
                MethodInfo method = type[0].GetMethod(libraryToLoad + "Service");
                method.Invoke(obj, Array.Empty<object>());
            }
        }
    }
}

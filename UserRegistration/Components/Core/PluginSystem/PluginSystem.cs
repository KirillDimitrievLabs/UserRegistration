using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UserRegistration.Components.PluginSystem
{
    public class PluginLoader
    {
        public static List<IPlugin> Plugins { get; set; }

        public void LoadPlugins(Dictionary<object, object>[] configDict)
        {
            Plugins = new List<IPlugin>();

            //Load the DLLs from the Plugins directory
            if (Directory.Exists("Plugins"))
            {
                string[] files = Directory.GetFiles("Plugins");
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFile(Path.GetFullPath(file));
                    }
                }
            }

            Type interfaceType = typeof(IPlugin);

            //Fetch all types that implement the interface IPlugin and are a class
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
                .ToArray();

            foreach (var config in configDict)
            {
                foreach (Type type in types)
                {
                    if (config["ConnectionName"].ToString() == type.Name)
                    {
                        object[] configObj = new object[] { config };
                        Plugins.Add((IPlugin)Activator.CreateInstance(type, configObj));
                    }
                }
            }
        }
    }
}

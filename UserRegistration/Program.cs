using System;
using UserRegistration.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Components.PluginSystem;
using UserRegistration.Components.Core;
using YamlDotNet.Serialization;

namespace UserRegistration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var deserializer = new DeserializerBuilder()
                .Build();

            List<UserSourceModel> userSourceModelList = deserializer.Deserialize<List<UserSourceModel>>(@"UserSource.yaml");
            Dictionary<object, object>[] configDictionary = deserializer.Deserialize<Dictionary<object, object>[]>("Config.yaml");

            if (configDictionary != null)
            {
                try
                {
                    PluginLoader loader = new PluginLoader();
                    loader.LoadPlugins(configDictionary);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Plugins couldn't be loaded: {e.Message}");
                    //TODO: repeater
                    //Environment.Exit(0);
                }
            }

            if (userSourceModelList != null)
            {
                try
                {
                    List<IPlugin> plugins = PluginLoader.Plugins;

                    foreach (var plugin in plugins)
                    {
                        if (plugin != null)
                        {
                            Syncer Syncer = new Syncer(userSourceModelList);
                            await Syncer.CreateUser(plugin);
                        }
                        else
                        {
                            Console.WriteLine($"No plugin found with name '{plugin.Name}'");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Caught exception: {e.Message}");
                }
            }
        }
    }
}
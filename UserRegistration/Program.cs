using System;
using UserRegistration.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Components.PluginSystem;
using UserRegistration.Components.Core;
using UserRegistration.Components.Utils;
using YamlDotNet.Serialization;
using System.IO;
using Microsoft.Extensions.Logging;

namespace UserRegistration
{
    class Program
    {
        private static Dictionary<object, object>[] ConfigDictionary { get; set; }
        private static UserSourceModel UserSourceModel { get; set; }
        private static ILoggerFactory _loggerFactory { get; set; }
        static async Task Main(string[] args)
        {
            ReadConfig("Config.yaml");
            _loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger _logger = _loggerFactory.CreateLogger<Program>();

            if (ConfigDictionary != null)
            {
                try
                {
                    UserSourceModel = new YamlConnection("UserSource.yaml").Read();


                    PluginLoader loader = new PluginLoader();
                    loader.LoadPlugins(ConfigDictionary);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Plugins couldn't be loaded: {e.Message}");
                }
            }

            List<IPlugin> plugins = PluginLoader.Plugins;
            IAsyncEnumerable<IPlugin> asyncPlugins = plugins.ToAsyncEnumerable();

            if (asyncPlugins != null)
            {
                await foreach (IPlugin plugin in asyncPlugins)
                {
                    Syncer syncer = new Syncer(UserSourceModel, plugin, _loggerFactory);

                    if (UserSourceModel.ServiceAction.ToLower() == "save")
                    {
                        await syncer.CreateUser();
                    }
                    else if (UserSourceModel.ServiceAction.ToLower() == "update")
                    {
                        await syncer.UpdateUser();
                    }
                    else if (UserSourceModel.ServiceAction.ToLower() == "delete")
                    {
                        await syncer.DeleteUser(args);
                    }
                    else
                    {
                        _logger.LogWarning($"Unknown {nameof(UserSourceModel.ServiceAction)}: {UserSourceModel.ServiceAction}");
                    }
                }
            }
            else
            {
                _logger.LogWarning($"No plugins found");
            }
            Console.ReadLine();
        }

        public static void ReadConfig(string path)
        {
            IDeserializer deserializer = new DeserializerBuilder()
                .Build();

            ConfigDictionary = deserializer.Deserialize<Dictionary<object, object>[]>(File.ReadAllText(path));
        }
    }
}
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
    //enum asd
    //{
    //    asd,
    //    olp,
    //    axc,
    //    asdcc
    //}
    class Program
    {
        
        private static ISource UserSourceModel { get; set; }
        private static ILoggerFactory _loggerFactory { get; set; }
        static async Task Main(string[] args)
        {
            Dictionary<object, object>[] configDictionary = ReadConfig("Config.yaml");
            _loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger _logger = _loggerFactory.CreateLogger<Program>();

            if (configDictionary != null)
            {
                try
                {
                    UserSourceModel = new YamlConnection("UserSource.yaml");

                    PluginLoader loader = new PluginLoader();
                    loader.LoadPlugins(configDictionary);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Plugins couldn't be loaded: {e.Message}");
                }
            }

            IDestination[] destinations = PluginLoader.Plugins.ToArray();

            if (destinations != null)
            {
                Syncer syncer = new Syncer(UserSourceModel, destinations, _loggerFactory);
                await syncer.Sync(args);
                //if (UserSourceModel.ServiceAction.ToLower() == "save")
                //{
                //    await syncer.CreateUser();
                //}
                //else if (UserSourceModel.ServiceAction.ToLower() == "update")
                //{
                //    await syncer.UpdateUser();
                //}
                //else if (UserSourceModel.ServiceAction.ToLower() == "delete")
                //{
                //    await syncer.DeleteUser(args);
                //}
            }
            else
            {
                throw new Exception($"No plugins found");
            }
        }

        public static Dictionary<object, object>[] ReadConfig(string path)
        {
            IDeserializer deserializer = new DeserializerBuilder()
                .Build();

            return deserializer.Deserialize<Dictionary<object, object>[]>(File.ReadAllText(path));
        }
    }
}
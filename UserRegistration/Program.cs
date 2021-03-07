using System;
using System.Reflection;
using UserRegistration.Components;
using UserRegistration.Models;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using YamlDotNet.Serialization;
using YamlDotNet.RepresentationModel;

namespace UserRegistration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //try
            //{
            //    await Loader.Load();
            //}
            //catch (HttpRequestException)
            //{
            //    Console.WriteLine(nameof(HttpRequestException));
            //}
            var yaml = File.ReadAllText("Config.yaml");
            var deserializer = new DeserializerBuilder().Build();
            Dictionary<object, object[]> result = deserializer.Deserialize<Dictionary<object, object[]>>(yaml);
            foreach (var item in result)
            {
                foreach (var item2 in item.Value)
                {
                    Console.WriteLine(item.Key.ToString() + ":");
                    foreach (var item3 in item2.)
                    {

                    }
                }
                
            }
        }
    }
}

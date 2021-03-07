using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Core;

namespace UserRegistration.Components
{
    public static class Yaml<T>
    {
        public static T YamlToModel(string fileName)
        {
            string yml = File.ReadAllText(fileName);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(NullNamingConvention.Instance)
                .Build();

            T model;
            try
            {
                model = deserializer.Deserialize<T>(yml);
                return model;
            }
            catch (YamlException e)
            {
                throw new YamlException(e.Message);
            }
        }
    }
}

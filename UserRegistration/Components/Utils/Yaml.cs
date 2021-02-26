using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace UserRegistration.Components
{
    public static class Yaml<T>
    {
        public static string GetPath(string fileName)
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory(), fileName, SearchOption.AllDirectories)[0];
        }

        public static T YamlToModel(string fileName)
        {
            string writePath = GetPath(fileName);
            string yml = @"";
            using (StreamReader sr = new StreamReader(writePath))
            {
                yml = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(NullNamingConvention.Instance)
                .Build();
            T model = deserializer.Deserialize<T>(yml);
            return model;
        }
        public static void ModelToYaml(T model, string fileName)
        {
            string writePath = GetPath(fileName);
            var serializer = new SerializerBuilder()
                .WithNamingConvention(NullNamingConvention.Instance)
                .Build();
            var yaml = serializer.Serialize(model);
            using (StreamWriter sw = new StreamWriter(writePath))
            {
                sw.Write(yaml);
            }
        }
    }
}

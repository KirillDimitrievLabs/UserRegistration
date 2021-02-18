using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace UserRegistration.Components
{
    class Yaml<T>
    {
        public static T YamlToModel(string path)
        {
            string writePath = $@"C:\Users\user\source\repos\UserRegistration\UserRegistration{path}";
            string yml = @"";
            using (StreamReader sr = new StreamReader(writePath))
            {
                yml = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder()
               .WithNamingConvention(UnderscoredNamingConvention.Instance)
               .Build();
            T model = deserializer.Deserialize<T>(yml);
            return model;
        }
        public static void ModelToYaml(T model, string path)
        {
            string writePath = $@"C:\Users\user\source\repos\UserRegistration\UserRegistration\{path}";
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var yaml = serializer.Serialize(model);
            using (StreamWriter sw = new StreamWriter(writePath))
            {
                sw.Write(yaml);
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

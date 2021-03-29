using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Models;
using YamlDotNet.Serialization;

namespace UserRegistration.Components.Core
{
    class YamlConnection : ISourceConnection
    {
        private string Path { get; set; }
        public YamlConnection(string path)
        {
            Path = path;
        }

        public UserSourceModel Read()
        {
            IDeserializer deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();

            return deserializer.Deserialize<UserSourceModel>(File.ReadAllText(Path));
        }
    }

    class Accounting : ISourceConnection
    {
        public UserSourceModel Read()
        {
            throw new NotImplementedException();
        }
    }
}

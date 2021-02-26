using System;
using System.Reflection;
using UserRegistration.Components;
using UserRegistration.Models;
using System.IO;
using YamlDotNet.Serialization;
using System.Collections.Generic;
using System.Collections;
using YamlDotNet.Serialization.NamingConventions;

namespace UserRegistration
{
    class Program
    {
        static void Main(string[] args)
        {
            Loader.Load();
        }
    }
}

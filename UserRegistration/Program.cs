using System;
using System.Reflection;
using UserRegistration.Components;
using UserRegistration.Models;
using System.IO;
using static UserRegistration.Models.Loader;

namespace UserRegistration
{
    class Program
    {
        static void Main(string[] args)
        {
            Loader.Load(@"AppConfig.yaml");
        }
    }
}

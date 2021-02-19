using System;
using System.Reflection;
using UserRegistration.Components;
using UserRegistration.Models;

namespace UserRegistration
{
    class Program
    {
        static void Main(string[] args)
        {
            Loader.Load("/AppConfig.yaml");
        }
    }
}

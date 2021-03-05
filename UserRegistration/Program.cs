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

namespace UserRegistration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await Loader.Load();
            }
            catch (HttpRequestException)
            {
                Console.WriteLine(nameof(HttpRequestException));
            }
        }
    }
}

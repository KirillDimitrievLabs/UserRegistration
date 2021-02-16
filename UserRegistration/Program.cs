using System;
using System.Collections.Generic;
using UserRegistration.UserWriters;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using UserRegistration.Models;
using UserRegistration.Components;

namespace UserRegistration
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new UserSourceToDestinationConverter();
            test.Convert();
            Console.WriteLine(test.Convert().Groups[0]);
        }
    }
}

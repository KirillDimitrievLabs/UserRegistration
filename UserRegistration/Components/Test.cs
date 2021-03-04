using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Models;

namespace UserRegistration.Components
{
    class Test
    {
        public static void Read()
        {
            var DLL = Assembly.LoadFile(System.IO.Directory.GetCurrentDirectory() + @"\YouTrack.dll");
            foreach (Type type in DLL.GetExportedTypes())
            {
                dynamic c = Activator.CreateInstance(type);
                c.Read();
            }
        }
    }
}

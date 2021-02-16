using System;
using System.Collections.Generic;
using UserRegistration.UserWriters;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using UserRegistration.Models;
using UserRegistration.Components;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using YouTrackSharp;
using System.Configuration;

namespace UserRegistration
{
    class Program
    {
        static void Main(string[] args)
        {
            YouTrack youtrack = new YouTrack();
        }
    }
}

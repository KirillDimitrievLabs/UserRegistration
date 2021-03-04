using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;

namespace UserRegistration.Models
{
    public class Syncer
    {
        private readonly ILogger _logger;

        public Syncer(ILogger<Syncer> logger)
        {
            _logger = logger;
        }

        private List<UserSourceModel> ReadUserSources()
        {
            List<UserSourceModel> userSourceModel = Yaml<List<UserSourceModel>>.YamlToModel(@"UserSource.yaml");
            _logger.LogInformation("Syncer.Read is called");
            return userSourceModel;
        }

        public List<UserDestinationModel> GetConvertedUsers()
        {
            List<UserSourceModel> userSource = ReadUserSources();
            _logger.LogInformation($"{nameof(Syncer)}.{nameof(GetConvertedUsers)} is called");
            
            return UserConverter.ToUserDestinationModel(userSource);
        }
        
        public static bool CompareUser(List<string> exsistingUserStr, string currentUserLogin)
        {
            return exsistingUserStr.Contains(currentUserLogin);
        }

        public static Syncer GetSyncer()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            var syncer = serviceProvider.GetService<Syncer>();
            return syncer;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
               .AddTransient<Syncer>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using System.Reflection;
using System.Dynamic;

namespace UserRegistration.Models
{
    public class Syncer
    {
        private readonly ILogger _logger;

        public Syncer(ILogger<Syncer> logger)
        {
            _logger = logger;
        }

        public static List<UserSourceModel> ReadUserSources()
        {
            List<UserSourceModel> userSourceModel = Yaml<List<UserSourceModel>>.YamlToModel(@"UserSource.yaml");
            return userSourceModel;
        }

        public async Task CreateUser(Assembly DLL)
        {
            foreach (Type type in DLL.GetExportedTypes())
            {
                List<UserDestinationModel> userDestinationList = UserConverter.ToUserDestinationModel(ReadUserSources());
                dynamic service = Activator.CreateInstance(type);
                List<string> exUserGroups = await service.ReadGroups();

                foreach (var user in userDestinationList)
                {
                    if (Syncer.CompareUser(await service.ReadUser(user), user.FullName) == false)
                    {
                        user.Groups = GetComparedUserGroups(exUserGroups, user.Groups);

                        await service.Save(user);

                        _logger.LogInformation($"{type.Name}: {user.FullName} created");

                        foreach (var group in user.Groups)
                        {
                            Syncer.GetSyncer()._logger.LogInformation($"Added to {group} group");
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"{type.Name}: {user.FullName} already exists");
                    }
                }
            }
        }

        public static bool CompareUser(List<string> exsistingUserStr, string currentUserLogin)
        {
            return exsistingUserStr.Contains(currentUserLogin);
        }

        public static string[] GetComparedUserGroups(List<string> exsistingGroupsList, string[] currentGroupsList)
        {
            string[] tempUserGroups = Array.Empty<string>();
            foreach (var userGroup in currentGroupsList)
            {
                if (exsistingGroupsList.Contains(userGroup))
                {
                    tempUserGroups.Append(userGroup);
                }
            }
            return tempUserGroups;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Components.PluginSystem;
using Microsoft.Extensions.Logging;
namespace UserRegistration.Components.Core
{
    public class Syncer
    {
        private static List<UserDestinationModel> ConvertedUserSources { get; set; }
        private IDestination[] Destinations { get; set; }
        private readonly ILogger _logger;

        public Syncer(ISource sourceConnection, IDestination[] destination, ILoggerFactory loggerFactory)
        {
            ConvertedUserSources = UserConverter.ToUserDestinationModel(sourceConnection.Read());
            
            Destinations = destination;
            _logger = loggerFactory.CreateLogger<Syncer>();
        }

        public async Task Sync(string[] args)
        {
            foreach (var convertedUserSource in ConvertedUserSources)
            {
                foreach (IDestination destination in Destinations)
                {
                    List<string> serviceUsers = await destination.ReadUsers();

                    if (!serviceUsers.Contains(convertedUserSource.FullName))
                    {
                        await CreateUser(destination, convertedUserSource);
                    }
                    else if (serviceUsers.Contains(convertedUserSource.FullName))
                    {
                        await UpdateUser(destination, convertedUserSource);
                    }
                }
            }
        }

        private async Task CreateUser(IDestination destination, UserDestinationModel userDestination)
        {
            List<string> existingUserGroups = await destination.ReadGroups();
            userDestination.Groups = GetComparedUserGroups(existingUserGroups, userDestination.Groups).ToArray();

            await destination.Save(userDestination);

            _logger.LogInformation($"{destination.GetType().Name}: User with name of '{userDestination.FullName}' was created");
        }

        private async Task UpdateUser(IDestination destination, UserDestinationModel userDestination)
        {

            await destination.Update(userDestination);

            _logger.LogInformation($"{destination.GetType().Name}: User with name of '{userDestination.FullName}' was updated");
        }

        private async Task DeleteUser(IDestination destination, UserDestinationModel userDestination, string[] args)
        {
            if (args.Contains("--force"))
            {
                await destination.Delete(userDestination);

                _logger.LogInformation($"{destination.GetType().Name}: User with name of '{userDestination.FullName}' was deleted");
            }
            else
            {
                _logger.LogWarning($"The user to be deleted: {userDestination.Login}");
            }
        }

        private static List<string> GetComparedUserGroups(List<string> exsistingGroupsList, string[] currentGroupsList)
        {
            List<string> tempUserGroups = new List<string>();
            foreach (string userGroup in currentGroupsList)
            {
                if (exsistingGroupsList.Contains(userGroup))
                {
                    tempUserGroups.Add(userGroup);
                }
            }
            return tempUserGroups;
        }
    }
}
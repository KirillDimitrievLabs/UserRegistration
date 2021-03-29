using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Components.PluginSystem;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
namespace UserRegistration.Components.Core
{
    public class Syncer
    {
        public static UserDestinationModel UserDestination { get; set; }
        private IPlugin Plugin { get; set; }
        private readonly ILogger _logger;

        public Syncer(UserSourceModel userSourceModel, IPlugin plugin, ILoggerFactory loggerFactory)
        {
            UserDestination = UserConverter.ToUserDestinationModel(userSourceModel);
            Plugin = plugin;
            _logger = loggerFactory.CreateLogger<Syncer>();
        }

        public async Task<string> CreateUser()
        {
            List<string> existingUserGroups = await Plugin.ReadGroups();
            List<string> existingUsers = await Plugin.ReadUsers();
            string result;

            if (!existingUsers.Contains(UserDestination.FullName))
            {
                UserDestination.Groups = GetComparedUserGroups(existingUserGroups, UserDestination.Groups).ToArray();

                await Plugin.Save(UserDestination);

                result = $"{Plugin.GetType().Name}: User with name of '{UserDestination.FullName}' was created";
                _logger.LogInformation(result);
                return result;
            }
            else
            {
                result = $"{Plugin.GetType().Name}: User with name of '{UserDestination.FullName}' already exists";
                _logger.LogError(result);
                return result;
            }
        }

        public async Task<string> UpdateUser()
        {
            List<string> existingUsers = await Plugin.ReadUsers();
            string result;

            if (existingUsers.Contains(UserDestination.FullName))
            {
                await Plugin.Update(UserDestination);

                result = $"{Plugin.GetType().Name}: User with name of '{UserDestination.FullName}' was updated";
                _logger.LogInformation(result);
                return result;
            }
            else
            {
                result = $"{Plugin.GetType().Name}: User with name of '{UserDestination.FullName}' does not exist";
                _logger.LogError(result);
                return result;
            }
        }

        public async Task<string> DeleteUser(string[] args)
        {
            List<string> existingUsers = await Plugin.ReadUsers();
            string result;

            if (existingUsers.Contains(UserDestination.FullName))
            {
                if (args.Contains("--force"))
                {
                    await Plugin.Delete(UserDestination);
                    result = $"{Plugin.GetType().Name}: User with name of '{UserDestination.FullName}' was deleted";
                    _logger.LogInformation(result);
                    return result;
                }
                else
                {
                    result = $"The user to be deleted: {UserDestination.Login}";
                    _logger.LogWarning(result);
                    return result;
                }
            }
            else
            {
                result = $"{Plugin.GetType().Name}: User with name of '{UserDestination.FullName}' does not exist";
                _logger.LogError(result);
                return result;
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
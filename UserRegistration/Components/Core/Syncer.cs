using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Components.PluginSystem;

namespace UserRegistration.Components.Core
{
    public class Syncer
    {
        //private readonly ILogger _logger;
        public static List<UserDestinationModel> UserDestinationModels { get; set; }

        public Syncer(List<UserSourceModel> userSourceModels)
        {
            UserDestinationModels = UserConverter.ToUserDestinationModel(userSourceModels);
            //_logger = logger;
        }

        //Creates user
        public async Task CreateUser(IPlugin plugin)
        {
            List<string> existingUserGroups = await plugin.ReadGroups();

            foreach (var user in UserDestinationModels)
            {
                if (CompareUser(await plugin.ReadUser(), user.FullName) == false)
                {
                    //Set compared groups
                    user.Groups = GetComparedUserGroups(existingUserGroups, user.Groups);

                    //Save user to service
                    await plugin.Save(user);


                    Console.WriteLine($"{plugin.GetType().Name}: {user.FullName} created");
                    //_logger.LogInformation($"{plugin.GetType().Name}: {user.FullName} created");

                    foreach (var group in user.Groups)
                    {
                        Console.WriteLine($"Added to {group} group");
                        //_logger.LogInformation($"Added to {group} group");
                    }
                }
                else
                {
                    Console.WriteLine($"{plugin.GetType().Name}: {user.FullName} already exists");
                    //_logger.LogWarning($"{plugin.GetType().Name}: {user.FullName} already exists");
                }
            }
        }

        //Сompares users from UserSource with exsisting users in service
        public bool CompareUser(List<string> exsistingUserStr, string currentUserLogin)
        {
            return exsistingUserStr.Contains(currentUserLogin);
        }

        //Compares groups from UserSource with exsisting groups in service
        private string[] GetComparedUserGroups(List<string> exsistingGroupsList, string[] currentGroupsList)
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
    }
}
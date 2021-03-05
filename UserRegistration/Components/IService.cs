using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistration.Models;

namespace UserRegistration.Components
{
    public interface IService
    {
        Task<List<string>> ReadUser();
        Task<List<string>> ReadGroups();
        Task Save(UserDestinationModel userToSave);
    }
}

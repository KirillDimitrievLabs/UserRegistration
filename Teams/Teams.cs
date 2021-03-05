using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistration.Components;
using UserRegistration.Models;

namespace Teams
{
    public class Teams : IService
    {
        private GraphServiceClient GetGraphClient()
        {
            var tenantId = "fd185bac-d7ba-412d-b46e-554186110ed4";
            var clientId = "d7424b64-e5a2-428e-b347-aaca025977b8";
            var clientSecret = ".~veKe4TI7V5HbDk5ydbz.c2skF~~AEwW5";
            //a23aec59-451c-4d84-8946-edda2b502138

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            return new GraphServiceClient(authProvider);
        }
        public async Task<List<string>> ReadGroups()
        {
            return new List<string>();
        }

        public Task<List<string>> ReadGroups(UserDestinationModel userToSave)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> ReadUser(UserDestinationModel userToSave)
        {
            throw new NotImplementedException();
        }

        public Task Save(UserDestinationModel userToSave)
        {
            throw new NotImplementedException();
        }
    }
}

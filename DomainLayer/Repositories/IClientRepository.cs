using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Repositories
{
    /// <summary>
    /// A collection of Client objects.
    /// </summary>
    interface IClientRepository
    {
        public void AddClient(Client client);
        public void GetClient(int clientId);
        public void UpdateClient(int id, Client udatedClient);
        public void DeleteClient(int id);
        
    }
}

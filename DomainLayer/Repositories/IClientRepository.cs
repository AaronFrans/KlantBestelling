using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Repositories
{
    /// <summary>
    /// A collection of Client objects in the database.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Add a client to the database
        /// </summary>
        /// <param name="client">Client to add.</param>
        public void AddClient(Client client);
        /// <summary>
        /// Retrieve a client froom the database.
        /// </summary>
        /// <param name="clientId">Id of the client.</param>
        public void GetClient(int clientId);
        /// <summary>
        /// Update a client in the database.
        /// </summary>
        /// <param name="id">Id of the client.</param>
        /// <param name="updatedClient">Updated Client.</param>
        public void UpdateClient(int id, Client updatedClient);
        /// <summary>
        /// Remove a client from the database.
        /// </summary>
        /// <param name="clientId">Id of the client to remove.</param>
        public void DeleteClient(int clientId);

    }
}

using DataLayer.DataLayerClasses;
using DomainLayer.Domain;
using DomainLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace DataLayer.Repositories
{
    public class ClientRepository : IClientRepository
    {

        private ClientOrderContext context;

        /// <summary>
        /// Constructor that establishes a connection between the database and the repository.
        /// </summary>
        /// <param name="context">Context to use.</param>
        public ClientRepository(ClientOrderContext context)
        {
            this.context = context;
        }


        public int AddClient(Client client)
        {
            DClient toAdd = Mapper.ToDClient(client);

            if (context.Clients.Any(c => c.Name == toAdd.Name && c.Addres == toAdd.Addres))
            {
                throw new DataException("Client is al in databank.");
            }
            context.Clients.Add(toAdd);
            context.SaveChanges();

            return toAdd.Id;
        }

        public void DeleteClient(int clientId)
        {
            if (!context.Clients.Any(c => c.Id == clientId))
            {
                throw new DataException("Het gegeven klantId is niet in de database");
            }

            if(context.Clients.AsNoTracking().Include(c => c.Orders).Single(c => c.Id == clientId).Orders.Count > 0)
            {
                throw new DataException("Er zijn nog bestellingen gelinkt aan de client");
            }

            context.Clients.Remove(new DClient() { Id = clientId });
        }

        public Client GetClient(int clientId)
        {
            if (!context.Clients.Any(c => c.Id == clientId))
            {
                throw new DataException("Het gegeven klantId is niet in de database");
            }

            return Mapper.ToClient(context.Clients.Single(c => c.Id == clientId));
        }

        public void UpdateClient(int id, Client updatedClient)
        {
            if (!context.Clients.Any(c => c.Id == id))
            {
                throw new DataException("Het gegeven klantId is niet in de database");
                
            }

            var toUpdateClient = context.Clients.Single(c => c.Id == id);

            toUpdateClient.Name = updatedClient.Name;
            toUpdateClient.Addres = updatedClient.Addres;

        }
    }
}

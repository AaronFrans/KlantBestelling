using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public class Manager: IManager
    {
        private IUnitOfWork uow;

        /// <summary>
        /// Makes a manager to comunicate between the data and domain layers.
        /// </summary>
        /// <param name="uow">Unit of work used to do the commands.</param>
        public Manager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public int AddClient(Client client)
        {
            uow.Clients.AddClient(client);
            uow.Complete();
            var toReturn = uow.Clients.GetClient(client.Name, client.Addres).Id;
            uow.Complete();
            return toReturn;
        }

        public Client GetClient(int clientId)
        {
            var client = uow.Clients.GetClient(clientId);
            uow.Complete();
            return client;
        }

        public void DeleteClient(int clientId)
        {
            uow.Clients.DeleteClient(clientId);
            uow.Complete();
        }

        public void UpdateClient(int clientId, Client updatedClient)
        {
            uow.Clients.UpdateClient(clientId, updatedClient);
            uow.Complete();
        }


        public int MakeOrder(int clientId, ProductType product, int amount)
        {
            uow.Orders.MakeOrder(clientId, product, amount);
            uow.Complete();
            var toReturn = uow.Orders.GetOrder(product, clientId).Id;
            uow.Complete();
            return toReturn;
        }


        public Order GetOrder(int orderId, int clientId)
        {
            var client = uow.Clients.GetClient(clientId);
            uow.Complete();

            var order = uow.Orders.GetOrder(orderId, clientId);
            uow.Complete();
            return order;
        }


        public void DeleteOrder(int orderId)
        {
            uow.Orders.DeleteOrder(orderId);
            uow.Complete();
        }

        public void UpdateOrder(int clientId, int orderId, ProductType product, int amount)
        {
            uow.Orders.UpdateOrder(clientId, orderId, product, amount);
            uow.Complete();
        }
    }
}

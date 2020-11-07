using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using static DomainLayer.Domain.ProductEnum;

namespace DomainLayer
{
    class Manager
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

        /// <summary>
        /// Add a client to the database.
        /// </summary>
        /// <param name="client">Client to add.</param>
        /// <returns>New id of the client.</returns>
        public int AddClient(Client client)
        {
            return uow.Clients.AddClient(client);
        }

        /// <summary>
        /// Get a client from the database.
        /// </summary>
        /// <param name="clientId">Id of the client.</param>
        /// <returns>The client that corresponds to the id.</returns>
        public Client GetClient(int clientId)
        {
            var client = uow.Clients.GetClient(clientId);
            uow.Complete();
            return client;
        }

        /// <summary>
        /// Remove a client from the database.
        /// </summary>
        /// <param name="clientId">Id of the client to remove.</param>
        public void DeleteClient(int clientId)
        {
            uow.Clients.DeleteClient(clientId);
            uow.Complete();
        }

        /// <summary>
        /// Update a client from the database.
        /// </summary>
        /// <param name="clientId">Id of the client to update.</param>
        public void UpdateClient(int clientId, Client updatedClient)
        {
            uow.Clients.UpdateClient(clientId, updatedClient);
            uow.Complete();
        }

        /// <summary>
        /// Add an order to the database.
        /// </summary>
        /// <param name="clientId">Id of the client that made the order.</param>
        /// <param name="product">Type of product ordered.</param>
        /// <param name="amount">Amount ordered.</param>
        /// <returns></returns>
        public int MakeOrder(int clientId, ProductType product, int amount)
        {
            return uow.Orders.MakeOrder(clientId, product, amount);
        }

        /// <summary>
        /// Get an order from the database.
        /// </summary>
        /// <param name="orderId">Id of the order.</param>
        /// <param name="clientId">Id of the client who made the order.</param>
        /// <returns>The order from the database that corresponds with the given order id.</returns>
        public Order GetOrder(int orderId, int clientId)
        {
            var order = uow.Orders.GetOrder(orderId, clientId);
            uow.Complete();
            return order;
        }


        /// <summary>
        /// Remove an order from the database.
        /// </summary>
        /// <param name="orderId">Id of the order to remove.</param>
        public void DeleteOrder(int orderId)
        {
            uow.Orders.DeleteOrder(orderId);
            uow.Complete();
        }

        /// <summary>
        ///  Update an order from the database.
        /// </summary>
        /// <param name="clientId">Id of the client who made the order.</param>
        /// <param name="orderId">Id of the order.</param>
        /// <param name="product">The product type of the order</param>
        /// <param name="amount">The new amount of the order.</param>
        public void UpdateOrder(int clientId, int orderId, ProductType product, int amount)
        {
            uow.Orders.UpdateOrder(clientId, orderId, product, amount);
            uow.Complete();
        }
    }
}

using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public interface IManager
    {
        public int AddClient(Client client);


        /// <summary>
        /// Get a client from the database.
        /// </summary>
        /// <param name="clientId">Id of the client.</param>
        /// <returns>The client that corresponds to the id.</returns>
        public Client GetClient(int clientId);


        /// <summary>
        /// Remove a client from the database.
        /// </summary>
        /// <param name="clientId">Id of the client to remove.</param>
        public void DeleteClient(int clientId);


        /// <summary>
        /// Update a client from the database.
        /// </summary>
        /// <param name="clientId">Id of the client to update.</param>
        public void UpdateClient(int clientId, Client updatedClient);


        /// <summary>
        /// Add an order to the database.
        /// </summary>
        /// <param name="clientId">Id of the client that made the order.</param>
        /// <param name="product">Type of product ordered.</param>
        /// <param name="amount">Amount ordered.</param>
        /// <returns></returns>
        public int MakeOrder(int clientId, ProductType product, int amount);


        /// <summary>
        /// Get an order from the database.
        /// </summary>
        /// <param name="orderId">Id of the order.</param>
        /// <param name="clientId">Id of the client who made the order.</param>
        /// <returns>The order from the database that corresponds with the given order id.</returns>
        public Order GetOrder(int orderId, int clientId);



        /// <summary>
        /// Remove an order from the database.
        /// </summary>
        /// <param name="orderId">Id of the order to remove.</param>
        public void DeleteOrder(int orderId);


        /// <summary>
        ///  Update an order from the database.
        /// </summary>
        /// <param name="clientId">Id of the client who made the order.</param>
        /// <param name="orderId">Id of the order.</param>
        /// <param name="product">The product type of the order</param>
        /// <param name="amount">The new amount of the order.</param>
        public void UpdateOrder(int clientId, int orderId, ProductType product, int amount);


    }
}

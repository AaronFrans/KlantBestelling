using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using static DomainLayer.Domain.ProductEnum;

namespace DomainLayer.Repositories
{
    /// <summary>
    /// A collection of Order objects in the database.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Make an order and add it to the database
        /// </summary>
        /// <param name="clientId">Id of the client who makes the order.</param>
        /// <param name="order">Th order</param>
        /// <returns>The new id int the database</returns>
        public int MakeOrder(int clientId, ProductType product, int amount);
        /// <summary>
        /// Retrieve an order froom the database.
        /// </summary>
        /// <param name="orderId">Id of the order.</param>
        /// <returns>The order from the database</returns>
        public Order GetOrder(int orderId, int clientId);
        /// <summary>
        /// Update an order in the database.
        /// </summary>
        /// <param name="clientId">Id of the client who makes the order. </param>
        /// <param name="orderId">Id of the order to update.</param>
        /// <param name="updatedOrder">Updated order.</param>
        public void UpdateOrder(int clientId, int orderId, ProductType product, int amount);
        /// <summary>
        /// Remove an order from the database.
        /// </summary>
        /// <param name="orderId">Id of the order to remove.</param>
        public void DeleteOrder(int orderId);
    }
}

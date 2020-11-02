using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

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
        public void MakeOrder(int clientId, Order order);
        /// <summary>
        /// Retrieve an order froom the database.
        /// </summary>
        /// <param name="orderId">Id of the order.</param>
        public void GetOrder(int orderId);
        /// <summary>
        /// Update an order in the database.
        /// </summary>
        /// <param name="clientId">Id of the client who makes the order. </param>
        /// <param name="orderId">Id of the order to update.</param>
        /// <param name="Order">Updated order.</param>
        public void UpdateOrder(int clientId, int orderId, Order updatedOrder);
        /// <summary>
        /// Remove an order from the database.
        /// </summary>
        /// <param name="orderId">Id of the order to remove.</param>
        public void DeleteOrder(int orderId);
    }
}

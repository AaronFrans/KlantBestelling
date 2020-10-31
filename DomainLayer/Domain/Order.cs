﻿using System;
using static DomainLayer.Domain.ProductEnum;

namespace DomainLayer.Domain
{
    /// <summary>
    /// An Order in the domain.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Id of the order.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Product ordered.
        /// </summary>
        public ProductType Product { get; private set; }
        /// <summary>
        /// Amount ordered.
        /// </summary>
        public int Amount { get; private set; }
        /// <summary>
        /// Client who ordered the order.
        /// </summary>
        public Client Client { get; private set; }

        /// <summary>
        /// Used to make a Order object.
        /// </summary>
        /// <param name="product">Product ordered.</param>
        /// <param name="amount">Amount ordered.</param>
        /// <param name="client">Client who ordered the order.</param>
        public Order(ProductType product, int amount, Client client)
        {
            Product = product;
            if (amount < 1)
                throw new DomainException("Het aantal moet groeter zijn dan 1");
            Amount = amount;
            Client = client;
        }

        /// <summary>
        /// Add an amount to the amount ordered.
        /// </summary>
        /// <param name="amount">Amount to add.</param>
        public void AddAmount(int amount)
        {
            Amount += amount;
        }

    }
}
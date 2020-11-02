
using System;
using System.Collections.Generic;
using System.Text;
using static DomainLayer.Domain.ProductEnum;

namespace DataLayer.DataLayerClasses
{
    /// <summary>
    /// An Order in the database.
    /// </summary>
    class DOrder
    {
        /// <summary>
        /// Id of the order.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Product ordered.
        /// </summary>
        public ProductType Product { get; set; }
        /// <summary>
        /// Amount ordered.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Used to make a Order object.
        /// </summary>
        /// <param name="product">Product ordered.</param>
        /// <param name="amount">Amount ordered.</param>
        /// <param name="client">Client who ordered the order.</param>
        public DOrder(ProductType product, int amount)
        {
            Product = product;
            Amount = amount;
        }
    }
}

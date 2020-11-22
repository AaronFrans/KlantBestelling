
using DomainLayer.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.DataLayerClasses
{
    /// <summary>
    /// An Order in the database.
    /// </summary>
    public class DOrder
    {
        /// <summary>
        /// Id of the order.
        /// </summary>
        [Key]
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
        /// Empty constructor for EF Core.
        /// </summary>
        public DOrder()
        {

        }

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

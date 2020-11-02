﻿using System;
using System.Collections.Generic;
using System.Linq;
using static DomainLayer.Domain.ProductEnum;

namespace DomainLayer.Domain
{
    /// <summary>
    /// A Client in the domain.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Id of the client.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the client.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Addres of the client.
        /// </summary>
        public string Addres { get; set; }
        /// <summary>
        /// Orders made by the client.
        /// </summary>
        public List<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// Used to make a Client object.
        /// </summary>
        /// <param name="name">Name of the client.</param>
        /// <param name="addres">Addres of the client.</param>
        public Client(string name, string addres)
        {
            Name = name;
            if (name == string.Empty)
                throw new DomainException("Een naam mag niet leeg zijn");
            if (addres.Length < 9)
                throw new DomainException("Een adres moet minstens 10 karakters lang zijn");
            Addres = addres;
        }

        /// <summary>
        /// Add a new order to the client.
        /// </summary>
        /// <param name="product">Product ordered.</param>
        /// <param name="amount">Amount ordered.</param>
        /// <param name="client">Client who ordered.</param>
        public static void AddOrder(ProductType product, int amount, Client client)
        {
            if (client.Orders.Any(o => o.Product == product))
                client.Orders.Single(o => o.Product == product).AddAmount(amount);
            else
                client.Orders.Add(new Order(product, amount, client));
        }

        public override bool Equals(object obj)
        {
            return obj is Client client &&
                   Name == client.Name &&
                   Addres == client.Addres;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Addres);
        }
    }
}

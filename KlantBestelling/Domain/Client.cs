using System;
using System.Collections.Generic;
using System.Linq;

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
        public string Name { get; private set; }
        /// <summary>
        /// Addres of the client.
        /// </summary>
        public string Addres { get; private set; }
        /// <summary>
        /// Orders made by the client.
        /// </summary>
        public List<Order> Orders { get; private set; } = new List<Order>();

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
            if (addres.Length < 10)
                throw new DomainException("Een adres moet minstens 10 karakters lang zijn");
            Addres = addres;
        }

        /// <summary>
        /// Add a new order to the client.
        /// </summary>
        /// <param name="product">Product ordered.</param>
        /// <param name="amount">Amount ordered.</param>
        public void AddOrder(ProductEnum product, int amount)
        {
            if (Orders.Any(o => o.Product == product))
                Orders.Single(o => o.Product == product).AddAmount(amount);
            else
                Orders.Add(new Order(product, amount, this));
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

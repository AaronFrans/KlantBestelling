using System;
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
        public string Name { get; private set; }
        /// <summary>
        /// Addres of the client.
        /// </summary>
        public string Addres { get; private set; }

        private List<Order> orders = new List<Order>();

        /// <summary>
        /// Orders made by the client.
        /// </summary>
        public IReadOnlyList<Order> Orders
        {
            get
            {
                return orders.AsReadOnly();
            }
        }

        /// <summary>
        /// Used to make a Client object.
        /// </summary>
        /// <param name="name">Name of the client.</param>
        /// <param name="addres">Addres of the client.</param>
        public Client(string name, string addres)
        {
            SetName(name);
            SetAddres(addres);
        }

        /// <summary>
        /// Add a new order to the client.
        /// </summary>
        /// <param name="product">Product ordered.</param>
        /// <param name="amount">Amount ordered.</param>
        public void AddOrder(ProductType product, int amount)
        {
            if (orders.Any(o => o.Product == product))
                orders.Single(o => o.Product == product).AddAmount(amount);
            else
                orders.Add(new Order(product, amount, this));
        }

        /// <summary>
        /// Change name of client.
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Een naam mag niet leeg zijn");
            Name = name;
        }

        /// <summary>
        /// Change addres of client.
        /// </summary>
        /// <param name="addres"></param>
        public void SetAddres(string addres)
        {
            if (string.IsNullOrWhiteSpace(addres))
                throw new DomainException("Een adres moet minstens 10 karakters lang zijn");
            if(addres.Length < 10)
                throw new DomainException("Een adres moet minstens 10 karakters lang zijn");
            Addres = addres;
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

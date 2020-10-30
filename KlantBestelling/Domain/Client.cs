using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainLayer.Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Addres { get; private set; }
        public List<Order> Orders { get; private set; } = new List<Order>();

        public Client(string name, string addres)
        {
            Name = name;
            if (name == string.Empty)
                throw new DomainException("Een naam mag niet leeg zijn");
            if (addres.Length < 10)
                throw new DomainException("Een adres moet minstens 10 karakters lang zijn");
            Addres = addres;
        }

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

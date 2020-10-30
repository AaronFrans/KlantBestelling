using System;

namespace DomainLayer.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public ProductEnum Product { get; private set; }
        public int Amount { get; private set; }
        public Client Client { get; private set; }

        public Order(ProductEnum product, int amount, Client client)
        {
            Product = product;
            if (amount < 1)
                throw new DomainException("Het aantal moet groeter zijn dan 1");
            Amount = amount;
            Client = client;
        }

        public void AddAmount(int amount)
        {
            Amount += amount;
        }

    }
}

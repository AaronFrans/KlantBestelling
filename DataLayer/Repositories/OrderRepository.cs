using DataLayer.DataLayerClasses;
using DomainLayer.Domain;
using DomainLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using static DomainLayer.Domain.ProductEnum;

namespace DataLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private ClientOrderContext context;

        public OrderRepository(ClientOrderContext context)
        {
            this.context = context;
        }

        public void DeleteOrder(int orderId)
        {
            if (!context.Orders.Any(c => c.Id == orderId))
            {
                throw new DataException("Het gegeven orderId is niet in de database");
            }



            context.Orders.Remove(new DOrder() { Id = orderId });
        }

        public Order GetOrder(int orderId, int clientId )
        {
            if (!context.Orders.AsNoTracking().Any(c => c.Id == orderId))
            {
                throw new DataException("Het gegeven orderId is niet in de database");
            }

            if (!context.Clients.AsNoTracking().Any(c => c.Id == clientId))
            {
                throw new DataException("Het gegeven klantId is niet in de database");
            }

            if(!context.Clients.AsNoTracking().Include(c=> c.Orders).Single(c => c.Id == clientId).Orders.Any(O => O.Id == orderId))
            {
                throw new DataException("Het gegeven klantId heeft geen order met het gegeven orderId");
            }


            var client = Mapper.ToClient(context.Clients.Single(c => c.Id == clientId));

            return Mapper.ToOrder(context.Orders.Single(c => c.Id == orderId), client);
        }

        public int MakeOrder(int clientId, ProductType product, int amount)
        {
            if (!context.Clients.Any(c => c.Id == clientId))
            {
                throw new DataException("The given clientId is not in the database");
            }

            var client = Mapper.ToClient(context.Clients.Single(c => c.Id == clientId));

            client.AddOrder(product, amount);

            var dOrder = Mapper.ToDOrder(client.Orders.Single(o => o.Product == product));

            context.Clients.Single(c => c.Id == clientId).Orders.Add(dOrder);

            context.SaveChanges();

            int toReturnId = dOrder.Id;

            return toReturnId;
        }

        public void UpdateOrder(int clientId, int orderId, ProductType product, int amount)
        {
            if (!context.Orders.Any(c => c.Id == orderId))
            {
                throw new DataException("Het gegeven orderId is niet in de database");
            }

            if (!context.Clients.Any(c => c.Id == clientId))
            {
                throw new DataException("Het gegeven klantId is niet in de database");
            }

            if (!context.Clients.Include(d=> d.Orders).Single(c => c.Id == clientId).Orders.Any(o=> o.Product == product))
            {
                throw new DataException("De gegeven klant heeft geen bestelling van het gegeven type.");
            }

            if(amount < 1)
            {
                throw new DataException("De hoeveelheid moet groter zijn dan 0");
            }

            if(!context.Clients.AsNoTracking().Include(c => c.Orders).Single(c => c.Id == clientId).Orders.Any(O => O.Id == orderId))
            {
                throw new DataException("Het gegeven klantId heeft geen order met het gegeven orderId");
            }


            var toUpdateOrder = context.Orders.Single(o => o.Id == orderId);

            if(toUpdateOrder.Product != product)
            {
                throw new DataException("Het product van het gegeven orderId is niet hetzelfde als het meegegeven product");
            }

            toUpdateOrder.Product = product;

            toUpdateOrder.Amount = amount;

        }
    }
}

using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataLayerClasses
{
    class Mapper
    {
        /// <summary>
        /// Transforms a Order object into a DOrder object.
        /// </summary>
        /// <param name="order">Order to transform.</param>
        /// <returns>The new DOrder object.</returns>
        public DOrder ToDOrder(Order order)
        {
            DOrder toReturn = new DOrder(order.Product, order.Amount);
            toReturn.Id = order.Id;
            return toReturn;
        }
        /// <summary>
        /// Transforms a Client object into a DClient object.
        /// </summary>
        /// <param name="client">Client to transform.</param>
        /// <returns>The new DClient object.</returns>
        public DClient ToDClient(Client client)
        {
            DClient toReturn = new DClient(client.Name, client.Addres);
            toReturn.Id = client.Id;
            List<DOrder> orders = new List<DOrder>();
            foreach (var item in client.Orders)
            {
                orders.Add(ToDOrder(item));

            }
            toReturn.Orders = orders;

            return toReturn;
        }

        /// <summary>
        /// Transforms a DClient object to a Client object.
        /// </summary>
        /// <param name="dClient">DClient to transforms.</param>
        /// <returns>The new Client object.</returns>
        public Client ToClient(DClient dClient)
        {
            Client toReturn = new Client(dClient.Name, dClient.Addres);
            toReturn.Id = dClient.Id;
            List<Order> orders = new List<Order>();
            foreach (var item in dClient.Orders)
            {
                orders.Add(ToOrder(item, toReturn));

            }
            toReturn.Orders = orders;

            return toReturn;
        }
        /// <summary>
        /// Transforms a DOrder object to a Order object.
        /// </summary>
        /// <param name="dOrder">DOrder to transforms.</param>
        /// <param name="client">Client who made the order.</param>
        /// <returns>The new Order object.</returns>
        public Order ToOrder(DOrder dOrder, Client client)
        {
            Order toReturn = new Order(dOrder.Product, dOrder.Amount, client);
            toReturn.Id = dOrder.Id;

            return toReturn;
        }
    }
}

using DomainLayer.Domain;
using RestLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLayer
{
    public class Mapper
    {
        /// <summary>
        /// Transforms a Client object into a RClientOutput object.
        /// </summary>
        /// <param name="client">Client to transform.</param>
        /// <returns>The new RClientOutput object.</returns>
        public static RClientOutput ToRClientOutput(Client client)
        {
            RClientOutput toReturn = new RClientOutput(client.Name, client.Addres);
            toReturn.Id = Urls.ClientUrl + client.Id;
            foreach(var order in client.Orders)
            {
                toReturn.Orders.Add(Urls.ClientUrl + client.Id + Urls.OrderUrl + order.Id);
            }    
            return toReturn;
        }

        /// <summary>
        /// Transforms a RClientInput object into a Client object.
        /// </summary>
        /// <param name="rClient">RCLient to transfrom.</param>
        /// <returns>The new Client object.</returns>
        public static Client ToClient(RClientInput rClient)
        {
            return new Client(rClient.Name, rClient.Addres);
        }

        /// <summary>
        /// Transforms a Order object into a ROrderOutput object.
        /// </summary>
        /// <param name="order">Order to transfrom.</param>
        /// <returns>The new ROrderOutput object.</returns>
        public static ROrderOutput ToROrderOutput(Order order)
        {
            var toReturn = new ROrderOutput(order.Product.ToString(), order.Amount, Urls.ClientUrl + order.Client.Id);
            toReturn.Id = Urls.ClientUrl + order.Client.Id + Urls.OrderUrl + order.Id;
            return toReturn;
        }


        public static ProductType ToProductType(string product)
        {
            ProductType toReturn;
            if(!Enum.TryParse<ProductType>(product,true, out toReturn))
                throw new RestException("Het gegeven product bestaat niet");
            return toReturn;
        }
    }
}

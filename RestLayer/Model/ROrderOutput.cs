using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestLayer.Model
{
    public class ROrderOutput
    {
        /// <summary>
        /// Id of the order.
        /// </summary>
        [JsonPropertyName("bestellingid")]
        public string Id { get; set; }
        /// <summary>
        /// Product ordered.
        /// </summary>
        public string Product { get; private set; }
        /// <summary>
        /// Amount ordered.
        /// </summary>
        [JsonPropertyName("aantal")]
        public int Amount { get; private set; }
        /// <summary>
        /// Client who ordered the order.
        /// </summary>
        [JsonPropertyName("klantid")]
        public string ClientId { get; private set; }


        /// <summary>
        /// Used to make an Order object.
        /// </summary>
        /// <param name="product">Product ordered.</param>
        /// <param name="amount">Amount ordered.</param>
        /// <param name="clientId">Client who ordered the order.</param>
        public ROrderOutput(string product, int amount, string clientId)
        {
            Product = product;
            Amount = amount;
            ClientId = clientId;
        }

        /// <summary>
        /// An empty constructor.
        /// </summary>
        public ROrderOutput()
        {

        }
    }
}

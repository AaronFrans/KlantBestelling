using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestLayer.Model
{
    public class ROrderInput
    {
        /// <summary>
        /// Id of the client.
        /// </summary>
        [JsonPropertyName("KlantId")]
        public int ClientId { get; set; }
        /// <summary>
        /// Id of the client.
        /// </summary>
        [JsonPropertyName("BestellingId")]
        public int OrderId { get; set; }
        /// <summary>
        /// Product ordered.
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// Amount ordered.
        /// </summary>
        [JsonPropertyName("Aantal")]
        public int Amount { get; set; }

        /// <summary>
        /// An empty constructor.
        /// </summary>
        public ROrderInput()
        {

        }
    }
}

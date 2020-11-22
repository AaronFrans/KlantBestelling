using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestLayer.Model
{
    /// <summary>
    /// A client in the rest service used for the input body
    /// </summary>
    public class RClientInput
    {
        /// <summary>
        /// Name of the client
        /// </summary>
        [JsonPropertyName("klantid")]
        public int Id { get; set; }
        /// <summary>
        /// Name of the client
        /// </summary>
        [JsonPropertyName("naam")]
        public string Name { get; set; }
        /// <summary>
        /// Addres of the client.
        /// </summary>
        [JsonPropertyName("adres")]
        public string Addres { get; set; }

        /// <summary>
        /// An empty constructor
        /// </summary>
        public RClientInput()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestLayer.Model
{
    /// <summary>
    /// A client in the rest service output body
    /// </summary>
    public class RClientOutput
    {
        /// <summary>
        /// Name of the client
        /// </summary>
        public string Id { get; set; }
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
        /// Orders made by the client.
        /// </summary>
        public List<string> Orders { get; set; } = new List<string>();

        /// <summary>
        /// An empty constructor
        /// </summary>
        public RClientOutput()
        {

        }

        /// <summary>
        /// Make a RClient object.
        /// </summary>
        /// <param name="name">Name of the client.</param>
        /// <param name="addres">Addres of the client.</param>
        public RClientOutput(string name, string addres)
        {
            Name = name;
            Addres = addres;
        }
    }
}

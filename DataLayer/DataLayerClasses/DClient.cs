using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using static DomainLayer.Domain.ProductEnum;

namespace DataLayer.DataLayerClasses
{
    public class DClient
    {
        /// <summary>
        /// Id of the client.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the client.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Addres of the client.
        /// </summary>
        public string Addres { get; set; }
        /// <summary>
        /// Orders made by the client.
        /// </summary>
        public List<DOrder> Orders { get; set; } = new List<DOrder>();


        /// <summary>
        /// Empty constructor for EF Core.
        /// </summary>
        public DClient()
        {

        }

        /// <summary>
        /// Used to make a Client object.
        /// </summary>
        /// <param name="name">Name of the client.</param>
        /// <param name="addres">Addres of the client.</param>
        public DClient(string name, string addres)
        {
            Name = name;
            Addres = addres;
        }

        public override bool Equals(object obj)
        {
            return obj is DClient client &&
                   Name == client.Name &&
                   Addres == client.Addres;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Addres);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DomainLayer.Domain.ProductEnum;

namespace DataLayer.DataLayerClasses
{
    class DClient
    {
        /// <summary>
        /// Id of the client.
        /// </summary>
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
        /// Used to make a Client object.
        /// </summary>
        /// <param name="name">Name of the client.</param>
        /// <param name="addres">Addres of the client.</param>
        public DClient(string name, string addres)
        {
            Name = name;
            if (name == string.Empty)
                throw new DataException("Een naam mag niet leeg zijn");
            if (addres.Length < 9)
                throw new DataException("Een adres moet minstens 10 karakters lang zijn");
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

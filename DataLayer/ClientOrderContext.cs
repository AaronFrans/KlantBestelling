using DataLayer.DataLayerClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    /// <summary>
    /// Connects to the database.
    /// </summary>
    class ClientOrderContext : DbContext
    {
        private string connectionString;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ClientOrderContext()
        {

        }

        /// <summary>
        /// Constructor used to connect to the database.
        /// </summary>
        /// <param name="db">Which database should be used, default is the production database.</param>
        public ClientOrderContext(string db = "Production") : base()
        {
            SetConnectionString(db);
        }

        /// <summary>
        /// Gets the connection string from the AppSettings.json file.
        /// </summary>
        /// <param name="db">Determines which database should be used. The default is the production database.</param>
        private void SetConnectionString(string db = "Production")
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(@"Files\appsettings.json", optional: false);

            var configuration = builder.Build();
            switch (db)
            {
                case "Production":
                    connectionString = configuration.GetConnectionString("ProdSQLconnection").ToString();
                    break;
                case "Test":
                    connectionString = configuration.GetConnectionString("TestSQLconnection").ToString();
                    break;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionString == null)
            {
                SetConnectionString();
            }
            optionsBuilder.UseSqlServer(connectionString);
        }

        /// <summary>
        /// Gives acces to the Orders in the database.
        /// </summary>
        public DbSet<DOrder> Orders {get; set;}

        /// <summary>
        /// Gives acces to the Clients in the database.
        /// </summary>
        public DbSet<DClient> Clients { get; set; }
    }
}

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
    public class ClientOrderContext : DbContext
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
            optionsBuilder.EnableSensitiveDataLogging();
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

    /// <summary>
    /// Used to easily test the functionality of the database.
    /// </summary>
    public class ClientOrderContextTest : ClientOrderContext
    {
        /// <summary>
        /// Empty contructor, it will connect  to the test databse.
        /// </summary>
        public ClientOrderContextTest() : base("Test")
        {

        }
        /// <summary>
        /// Constructor used to connect to the test database.
        /// </summary>
        /// <param name="keepExistingDB">Determines if the test database should be emptied. It will delete by default.</param>
        public ClientOrderContextTest(bool keepExistingDB = false) : base("Test")
        {
            if (keepExistingDB)
            {
                Database.EnsureCreated();
            }
            else
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }


        }
    }
}

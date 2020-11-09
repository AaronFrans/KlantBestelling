using DataLayer.Repositories;
using DomainLayer;
using DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {

        private ClientOrderContext context;

        public IOrderRepository Orders { get; private set; }

        public IClientRepository Clients { get; private set; }

        public UnitOfWork(ClientOrderContext context)
        {
            this.context = context;
            Orders = new OrderRepository(context);
            Clients = new ClientRepository(context);
        }

        public int Complete()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Disposes of the context.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}

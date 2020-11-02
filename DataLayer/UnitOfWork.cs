using DomainLayer;
using DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    class UnitOfWork : IUnitOfWork
    {
        public IOrderRepository Orders => throw new NotImplementedException();

        public IClientRepository Clients => throw new NotImplementedException();

        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

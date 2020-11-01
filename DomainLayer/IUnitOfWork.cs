using DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    interface IUnitOfWork : IDisposable
    {
        public IClientRepository Clients {get;}
        int Complete();
    }
}

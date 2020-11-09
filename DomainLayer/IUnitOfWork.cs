using DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    /// <summary>
    /// Gives commands to the database.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Orders in the database.
        /// </summary>
        public IOrderRepository Orders { get; }
        /// <summary>
        /// Clients in the database.
        /// </summary>
        public IClientRepository Clients { get; }
        /// <summary>
        /// Complete tasks done by the database.
        /// </summary>
        /// <returns>An int signifying whether the task was succsefull.</returns>
        int Complete();
    }
}

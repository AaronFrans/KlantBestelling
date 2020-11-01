using DomainLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Repositories
{
    interface IOrderRepository
    {
        public void MakeOrder(int clientId, Order Order);
        public void GetOrder(int orderId);
        public void UpdateOrder(int clientId, int orderId, Order Order);
        public void DeleteOrder(int orderId);
    }
}

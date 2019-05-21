using Book.Data.Entities;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Book.Services
{
    public interface IOrderService
    {
        void AddOrder(int userId, int bookId, string buyerName, string phoneNumber, string address);

        void RemoveOrders(Expression<Func<OrderEntity, bool>> predicate);

        List<OrderModel> GetOrders(Expression<Func<OrderEntity, bool>> predicate);
    }
}

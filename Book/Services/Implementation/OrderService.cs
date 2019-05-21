using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Book.Data.Context;
using Book.Data.Entities;
using Book.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly MySqlContext _mySql;

        public OrderService(MySqlContext mySql)
        {
            _mySql = mySql;
        }

        public void AddOrder(int userId, int bookId, string buyerName, string phoneNumber, string address)
        {
            _mySql.AddAsync(new OrderEntity
            {
                BuyerId = userId,
                BookId = bookId,
                BuyerName = buyerName,
                PhoneNumber = phoneNumber,
                Address = address,
                TimeStamp = DateTime.Now
            });
            _mySql.SaveChangesAsync();
        }

        public List<OrderModel> GetOrders(Expression<Func<OrderEntity, bool>> predicate)
        {
            var orderList = _mySql.Orders.
                Where(predicate).
                ToList();
            var res = new List<OrderModel>();
            foreach (var order in orderList)
            {
                res.Add(new OrderModel(order));
            }
            return res;
        }

        public void RemoveOrders(Expression<Func<OrderEntity, bool>> predicate)
        {
            var orders = _mySql.Orders.
                Where(predicate).
                ToArray();
            _mySql.Orders.RemoveRange(orders);
            _mySql.SaveChangesAsync();
        }
    }

    public static class OrderServiceExtensions
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services)
        {
            return services.AddScoped<IOrderService, OrderService>();
        }
    }
}

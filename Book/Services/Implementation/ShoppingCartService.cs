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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly MySqlContext _mySql;

        public ShoppingCartService(MySqlContext mySql)
        {
            _mySql = mySql;
        }

        public void AddInCart(int userId, int bookId, int number)
        {
            var sell = _mySql.Sells.
                Where(entity => entity.Id == bookId).
                ToArray();
            if (sell.Length != 1)
                throw new Exception("Sell not exists.");

            if (sell[0].Remaining < number)
                throw new Exception("Not enough books.");

            sell[0].Remaining -= number;
            _mySql.Sells.Update(sell[0]);

            _mySql.AddAsync(new ShoppingCartEntity
            {
                UserId = userId,
                BookId = bookId,
                Number = number
            });
            _mySql.SaveChangesAsync();
        }

        public List<SellModel> GetCartRecords(Expression<Func<ShoppingCartEntity, bool>> predicate)
        {
            var books = _mySql.ShoppingCarts.
                Where(predicate).
                Select(entity => entity.BookId).
                ToList();

            var res = new List<SellModel>();
            foreach (var bookId in books)
            {
                res.Add(new SellModel(_mySql.Sells.
                    FirstOrDefault(entity => entity.Id == bookId)));
            }

            return res;
        }

        public void RemoveCarts(Expression<Func<ShoppingCartEntity, bool>> predicate)
        {
            var carts = _mySql.ShoppingCarts.
                Where(predicate).
                ToArray();

            foreach (var cart in carts)
            {
                var sell = _mySql.Sells.
                    Where(entity => entity.Id == cart.BookId).
                    ToArray();
                if (sell.Length != 1) continue;
                sell[0].Remaining += cart.Number;
                _mySql.Sells.Update(sell[0]);
            }

            _mySql.ShoppingCarts.RemoveRange(carts);
            _mySql.SaveChangesAsync();
        }
    }

    public static class ShoppingCartServiceExtensions
    {
        public static IServiceCollection AddShoppingCartService(this IServiceCollection services)
        {
            return services.AddScoped<IShoppingCartService, ShoppingCartService>();
        }
    }
}

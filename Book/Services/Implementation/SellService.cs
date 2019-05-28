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
    public class SellService : ISellService
    {
        private readonly MySqlContext _mySql;

        public SellService(MySqlContext mySql)
        {
            _mySql = mySql;
        }

        public async Task AddSell(int sellerId, string bookName, int remaining, int category,
            string imageURL, string press, string author, DateTime publishedDate, int depreciation,
            string ISBN, double price, string description)
        {
            await _mySql.Sells.AddAsync(new BookEntity
            {
                SellerId = sellerId,
                Name = bookName,
                Remaining = remaining,
                Category = category,
                ImageURL = imageURL,
                Press = press,
                Author = author,
                PublishedDate = publishedDate,
                Depreciation = depreciation,
                ISBN = ISBN,
                Price = price,
                Description = description
            });
            await _mySql.SaveChangesAsync();
        }

        public List<SellModel> GetSells(Expression<Func<BookEntity, bool>> predicate)
        {
            var sells = _mySql.Sells.
                Where(predicate).
                ToList();
            var res = new List<SellModel>();
            foreach (var sell in sells)
            {
                res.Add(new SellModel(sell));
            }
            return res;
        }

        public async Task RemoveSells(Expression<Func<BookEntity, bool>> predicate)
        {
            var sells = _mySql.Sells.
                Where(predicate).
                ToArray();
            _mySql.Sells.RemoveRange(sells);
            await _mySql.SaveChangesAsync();
        }
    }

    public static class SellServiceExtensions
    {
        public static IServiceCollection AddSellService(this IServiceCollection services)
        {
            return services.AddScoped<ISellService, SellService>();
        }
    }
}

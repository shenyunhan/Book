using Book.Data.Entities;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Book.Services
{
    public interface ISellService
    {
        Task AddSell(int sellerId, string bookName, int remaining, int category,
            string imageURL, string press, string author, DateTime publishedDate,
            int depreciation, string ISBN, double price, string description);

        Task RemoveSells(Expression<Func<BookEntity, bool>> predicate);

        List<SellModel> GetSells(Expression<Func<BookEntity, bool>> predicate);
    }
}

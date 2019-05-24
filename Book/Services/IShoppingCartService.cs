using Book.Data.Entities;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Book.Services
{
    public interface IShoppingCartService
    {
        void AddInCart(int userId, int bookId, int number);

        void RemoveCarts(Expression<Func<ShoppingCartEntity, bool>> predicate);

        List<SellModel> GetCartRecords(Expression<Func<ShoppingCartEntity, bool>> predicate);
    }
}

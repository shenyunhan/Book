using Book.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Book.Services
{
    public interface IShoppingCartService
    {
        bool AddInCart(int userId, int bookId, int number);

        void RemoveCarts(Expression<Func<ShoppingCartEntity, bool>> predicate);

        List<int> GetCartRecords(Expression<Func<ShoppingCartEntity, bool>> predicate);
    }
}

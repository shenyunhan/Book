using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Book.Extensions
{
    public class EFCoreExtensions
    {
        public static Expression<Func<TEntity, bool>> BuildContainsExpression<TEntity, TValue>(
            Expression<Func<TEntity, TValue>> selecter, IEnumerable<TValue> values)
        {
            var contains = values.Select(value => (Expression)Expression.
                Call(selecter.Body, typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                Expression.Constant(value, typeof(TValue))));
            var body = contains.Aggregate((accumulate, equal) => Expression.And(accumulate, equal));
            return Expression.Lambda<Func<TEntity, bool>>(body, Expression.Parameter(typeof(TEntity)));
        }
    }
}

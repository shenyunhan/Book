using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Book.Data.Context;
using Book.Data.Entities;
using Book.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Services.Implementation
{
    public class RewardService : IRewardService
    {
        private readonly MySqlContext _mySql;

        public RewardService(MySqlContext mySql)
        {
            _mySql = mySql;
        }

        public void AddReward(int userId, string bookName, string press, int category, string author,
            string ISBN, double price, string imageURL, DateTime publishedDate, int depreciation, string description)
        {
            _mySql.Rewards.AddAsync(new RewardEntity
            {
                UserId = userId,
                BookName = bookName,
                Press = press,
                Category = category,
                Author = author,
                ISBN = ISBN,
                Price = price,
                ImageURL = imageURL,
                PublishedDate = publishedDate,
                Depreciation = depreciation,
                Description = description
            });
            _mySql.SaveChangesAsync();
        }

        public List<RewardModel> GetRewards(Expression<Func<RewardEntity, bool>> predicate)
        {
            var rewards = _mySql.Rewards.
                Where(predicate).
                ToList();
            var res = new List<RewardModel>();
            foreach (var reward in rewards)
            {
                res.Add(new RewardModel(reward));
            }
            return res;
        }

        public void RemoveRewards(Expression<Func<RewardEntity, bool>> predicate)
        {
            var reward = _mySql.Rewards.
                Where(predicate).
                ToArray();
            _mySql.Rewards.RemoveRange(reward);
            _mySql.SaveChangesAsync();
        }
    }

    public static class RewardServiceExtensions
    {
        public static IServiceCollection AddRewardService(this IServiceCollection services)
        {
            return services.AddScoped<IRewardService, RewardService>();
        }
    }
}

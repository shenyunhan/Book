using Book.Data.Entities;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Book.Services
{
    public interface IRewardService
    {
        void AddReward(int userId, string bookName, string press, int category, string author,
            string ISBN, double price, string imageURL, DateTime publishedDate, int depreciation, string description);

        void RemoveRewards(Expression<Func<RewardEntity, bool>> predicate);

        List<RewardModel> GetRewards(Expression<Func<RewardEntity, bool>> predicate);
    }
}

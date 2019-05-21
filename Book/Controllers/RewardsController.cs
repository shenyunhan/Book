using System;
using System.Collections.Generic;
using Book.Models;
using Book.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {
        private readonly IUserService _users;
        private readonly IRewardService _rewards;

        public RewardsController(IUserService users, IRewardService rewards)
        {
            _users = users;
            _rewards = rewards;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<RewardModel>> GetAll()
        {
            return _rewards.GetRewards(entity => true);
        }

        [HttpGet("getMy")]
        public ActionResult<IEnumerable<RewardModel>> GetByUser(string openId)
        {
            int userId = _users.GetIdByOpenId(openId);
            return _rewards.GetRewards(entity => entity.UserId == userId);
        }

        [HttpDelete("cancel")]
        public ActionResult<bool> DeleteById([FromBody] string raw)
        {
            var json = JObject.Parse(raw);

            var id = (int)json["id"];
            _rewards.RemoveRewards(entity => entity.Id == id);
            return true;
        }

        [HttpPost("add")]
        public ActionResult<bool> Post([FromBody] string raw)
        {
            var json = JObject.Parse(raw);

            var userId = _users.GetIdByOpenId((string)json["openId"]);
            var bookName = (string)json["bookName"];
            var category = (int)json["category"];
            var imageURL = (string)json["imageURL"];
            var press = (string)json["press"];
            var author = (string)json["author"];
            var publishedDate = (DateTime)json["publishedDate"];
            var depreciation = (int)json["depreciation"];
            var ISBN = (string)json["ISBN"];
            var price = (double)json["price"];
            var description = (string)json["description"];

            _rewards.AddReward(userId, bookName, press, category, author,
                ISBN, price, imageURL, publishedDate, depreciation, description);

            return true;
        }
    }
}
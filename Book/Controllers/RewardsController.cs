using System;
using System.Collections.Generic;
using Book.Extensions;
using Book.Models;
using Book.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IRewardService _rewards;

        public RewardsController(IHttpContextAccessor accessor, IRewardService rewards)
        {
            _accessor = accessor;
            _rewards = rewards;
        }

        [HttpGet("getAll")]
        public ActionResult<ResultModel> GetAll()
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();

                System.Console.WriteLine($"All rewards' info got by user {userId}.");

                return ResultModel.Success(_rewards.GetRewards(entity => true));
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        [HttpGet("getMy")]
        public ActionResult<ResultModel> GetByUser()
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();

                System.Console.WriteLine($"User {userId}'s rewards got.");

                return ResultModel.Success(_rewards.GetRewards(entity => entity.UserId == userId));
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        [HttpGet("getByCondition")]
        public ActionResult<ResultModel> GetByCondition([FromQuery] int? category, [FromQuery] string word)
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();
                return ResultModel.Success(_rewards.
                    GetRewards(entity =>
                    (category == null ? true : entity.Category == category) &&
                    (word == null ? true : entity.BookName.Contains(word))));
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        [HttpDelete("cancel")]
        public ActionResult<ResultModel> DeleteById([FromBody] JObject json)
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();

                var id = (int)json["rewardId"];
                _rewards.RemoveRewards(entity => entity.Id == id);

                System.Console.WriteLine($"Reward canceled by user {userId}.");

                return ResultModel.Success();
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        [HttpPost("add")]
        public ActionResult<ResultModel> Post([FromBody] JObject json)
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();
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

                System.Console.WriteLine($"Reward added by user {userId}.");

                return ResultModel.Success();
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class SellsController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ISellService _sells;

        public SellsController(IHttpContextAccessor accessor, ISellService sells)
        {
            _accessor = accessor;
            _sells = sells;
        }

        // Get api/sells/getAll
        [HttpGet("getAll")]
        public ActionResult<ResultModel> GetAll()
        {
            try
            {
                System.Console.WriteLine("Start getting sells' info.");

                var userId = _accessor.HttpContext.GetUserId();

                System.Console.WriteLine($"All sells' info got by user {userId}.");

                return ResultModel.Success(_sells.GetSells(entity => entity.Remaining > 0));
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        // Get api/sells/getMy
        [HttpGet("getMy")]
        public ActionResult<ResultModel> GetByUser()
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();

                return ResultModel.Success(_sells.GetSells(entity => entity.SellerId == userId));
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        [HttpGet("getById")]
        public ActionResult<ResultModel> GetById([FromQuery] int id)
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();
                var sell = _sells.GetSells(entity => entity.Id == id);
                if (sell.Count == 0)
                    return ResultModel.Success();
                return ResultModel.Success(sell[0]);
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        [HttpGet("getByCondition")]
        public ActionResult<ResultModel> GetByCondition([FromQuery] int? category, [FromQuery] string words)
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();
                return ResultModel.Success(_sells.
                    GetSells(entity => entity.Remaining > 0 &&
                    (category == null ? true : entity.Category == category) &&
                    (words == null ? true : entity.Name.Contains(words))));
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ResultModel>> Post([FromBody] JObject json)
        {
            try
            {
                var sellerId = _accessor.HttpContext.GetUserId();
                var bookName = (string)json["bookName"];
                var remaining = (int)json["remaining"];
                var category = (int)json["category"];
                var imageURL = (string)json["imageURL"];
                var press = (string)json["press"];
                var author = (string)json["author"];
                var publishedDate = (DateTime)json["publishedDate"];
                var depreciation = (int)json["depreciation"];
                var ISBN = (string)json["ISBN"];
                var price = (double)json["price"];
                var description = (string)json["description"];

                await _sells.AddSell(sellerId, bookName, remaining, category, imageURL,
                    press, author, publishedDate, depreciation, ISBN, price, description);

                System.Console.WriteLine($"New sell added by user {sellerId}.");

                return ResultModel.Success();
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }
    }
}
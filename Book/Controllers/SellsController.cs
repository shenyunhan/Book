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
    public class SellsController : ControllerBase
    {
        private readonly IUserService _users;
        private readonly ISellService _sells;

        public SellsController(IUserService users, ISellService sells)
        {
            _users = users;
            _sells = sells;
        }

        // Get api/sells/getAll
        [HttpGet("getAll")]
        public ActionResult<IEnumerable<SellModel>> GetAll()
        {
            return _sells.GetSells(entity => entity.Remaining > 0);
        }

        // Get api/sells/getMy
        [HttpGet("getMy")]
        public ActionResult<IEnumerable<SellModel>> GetByUser([FromQuery] string openId)
        {
            var userId = _users.GetIdByOpenId(openId);

            return _sells.GetSells(entity => entity.SellerId == userId);
        }

        [HttpGet("getById")]
        public ActionResult<SellModel> GetById([FromQuery] int id)
        {
            var sell = _sells.GetSells(entity => entity.Id == id);
            if (sell.Count == 0)
                return null;
            return sell[0];
        }

        // Get api/sells/getByCategory
        [HttpGet("getByCategory")]
        public ActionResult<IEnumerable<SellModel>> GetByCategory([FromQuery] int category)
        {
            return _sells.GetSells(entity => entity.Category == category && entity.Remaining > 0);
        }

        [HttpPost("add")]
        public ActionResult<bool> Post([FromBody] string raw)
        {
            var json = JObject.Parse(raw);

            var sellerId = _users.GetIdByOpenId((string)json["openId"]);
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

            _sells.AddSell(sellerId, bookName, remaining, category, imageURL,
                press, author, publishedDate, depreciation, ISBN, price, description);

            return true;
        }
    }
}
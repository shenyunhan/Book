using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IUserService _users;
        private readonly IShoppingCartService _carts;

        public ShoppingCartController(IUserService users, IShoppingCartService carts)
        {
            _users = users;
            _carts = carts;
        }

        [HttpGet("getMy")]
        public ActionResult<IEnumerable<int>> GetByUser([FromQuery] string openId)
        {
            var userId = _users.GetIdByOpenId(openId);
            return _carts.GetCartRecords(entity => entity.UserId == userId);
        }

        [HttpPost("add")]
        public ActionResult<bool> Post([FromBody] JObject json)
        {
            var userId = _users.GetIdByOpenId((string)json["openId"]);
            var bookId = (int)json["boolId"];
            var number = (int)json["number"];

            _carts.AddInCart(userId, bookId, number);

            return true;
        }
    }
}
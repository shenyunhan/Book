using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Models;
using Book.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUserService _users;
        private readonly IOrderService _orders;

        public OrdersController(IUserService users, IOrderService orders)
        {
            _users = users;
            _orders = orders;
        }

        [HttpGet("getMy")]
        public ActionResult<IEnumerable<OrderModel>> GetByUser([FromQuery] string openId)
        {
            var userId = _users.GetIdByOpenId(openId);
            return _orders.GetOrders(entity => entity.BuyerId == userId);
        }

        [HttpPost("add")]
        public ActionResult<bool> Post([FromBody] JObject json)
        {
            var userId = _users.GetIdByOpenId((string)json["openId"]);
            var bookId = (int)json["bookId"];
            var buyerName = (string)json["buyerName"];
            var phoneNumber = (string)json["phoneNumber"];
            var address = (string)json["address"];

            _orders.AddOrder(userId, bookId, buyerName, phoneNumber, address);
            return true;
        }
    }
}
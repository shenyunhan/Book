using System;
using System.Collections.Generic;
using System.Linq;
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
    public class OrdersController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IOrderService _orders;

        public OrdersController(IHttpContextAccessor accessor, IOrderService orders)
        {
            _accessor = accessor;
            _orders = orders;
        }

        [HttpGet("getMy")]
        public ActionResult<ResultModel> GetByUser()
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();

                System.Console.WriteLine($"User {userId}'s orders got.");

                return ResultModel.Success(_orders.GetOrders(entity => entity.BuyerId == userId));
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        [HttpPost("addDirectly")]
        public ActionResult<ResultModel> PostDirectly([FromBody] JObject json)
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();
                var bookId = (int)json["bookId"];
                var number = (int)json["number"];
                var buyerName = (string)json["buyerName"];
                var phoneNumber = (string)json["phoneNumber"];
                var address = (string)json["address"];

                _orders.AddOrderDirectly(userId, bookId, number, buyerName, phoneNumber, address);

                System.Console.WriteLine($"Book {bookId} is ordered by user {userId}.");

                return ResultModel.Success();
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }

        [HttpPost("addFromCart")]
        public ActionResult<ResultModel> PostFromCart([FromBody] JObject json)
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();
                var bookId = (int)json["bookId"];
                var buyerName = (string)json["buyerName"];
                var phoneNumber = (string)json["phoneNumber"];
                var address = (string)json["address"];

                _orders.AddOrderFromCart(userId, bookId, buyerName, phoneNumber, address);

                System.Console.WriteLine($"Book {bookId} is ordered by user {userId}.");

                return ResultModel.Success();
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }
    }
}
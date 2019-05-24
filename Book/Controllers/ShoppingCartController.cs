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
    public class ShoppingCartController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IShoppingCartService _carts;

        public ShoppingCartController(IHttpContextAccessor accessor, IShoppingCartService carts)
        {
            _accessor = accessor;
            _carts = carts;
        }

        [HttpGet("getMy")]
        public ActionResult<ResultModel> GetByUser()
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();

                System.Console.WriteLine($"Shopping cart info got by user {userId}.");

                return ResultModel.Success(_carts.GetCartRecords(entity => entity.UserId == userId));
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
                var bookId = (int)json["bookId"];
                var number = (int)json["number"];

                _carts.AddInCart(userId, bookId, number);

                System.Console.WriteLine($"Book {bookId} is added into user {userId}'s shopping cart.");

                return ResultModel.Success();
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }
    }
}
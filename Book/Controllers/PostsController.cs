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
    public class PostsController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IPostService _posts;

        public PostsController(IHttpContextAccessor accessor, IPostService posts)
        {
            _accessor = accessor;
            _posts = posts;
        }

        [HttpGet("getAll")]
        public ActionResult<ResultModel> GetAll()
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();

                System.Console.WriteLine($"All posts got by user {userId}.");

                return ResultModel.Success(_posts.GetPosts(entity => true));
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

                System.Console.WriteLine($"{userId}'s posts got.");

                return ResultModel.Success(_posts.GetPosts(entity => entity.AuthorId == userId));
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
                var title = (string)json["title"];
                var content = (string)json["content"];
                _posts.AddPost(userId, title, content);

                System.Console.WriteLine($"Post added by user {userId}.");

                return ResultModel.Success();
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }
    }
}
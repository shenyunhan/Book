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
    public class CommentsController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ICommentService _comments;

        public CommentsController(IHttpContextAccessor accessor, ICommentService comments)
        {
            _accessor = accessor;
            _comments = comments;
        }

        [HttpGet("getByPost")]
        public ActionResult<ResultModel> GetByPost([FromQuery] int postId)
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();

                System.Console.WriteLine($"Post {postId}'s comments got by user {userId}.");

                return ResultModel.Success(_comments.GetComments(entity => entity.PostId == postId));
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
                var postId = (int)json["postId"];
                var content = (string)json["content"];
                _comments.AddComment(postId, userId, content);

                System.Console.WriteLine($"Comment added to Post {postId} by user {userId}.");

                return ResultModel.Success();
            }
            catch (Exception e)
            {
                return ResultModel.Fail(e.Message);
            }
        }
    }
}
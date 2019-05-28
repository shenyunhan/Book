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
    public class UsersController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IWechatService _wechat;
        private readonly IUserService _users;

        public UsersController(IHttpContextAccessor accessor, IWechatService wechat, IUserService users)
        {
            _accessor = accessor;
            _wechat = wechat;
            _users = users;
        }

        [HttpPut("login")]
        public async Task<ActionResult<ResultModel>> Login([FromBody] JObject json)
        {
            try
            {
                var code = (string)json["code"];
                var nickName = (string)json["nickName"];
                var imageURL = (string)json["imageURL"];

                var session = _wechat.Code2Session(code);

                if (session["errcode"] != null)
                    throw new Exception((string)session["errmsg"]);

                var openId = (string)session["openid"];

                if (_users.FindUser(openId))
                    await _users.UpdateUser(openId, nickName, imageURL);
                else await _users.AddUser(openId, nickName, imageURL);

                int userId = _users.GetIdByOpenId(openId);
                _accessor.HttpContext.SetUserKey(userId);

                System.Console.WriteLine($"New connection, userID = {userId}.");

                return ResultModel.Success();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return ResultModel.Fail(e.Message);
            }
        }
    }
}
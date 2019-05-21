using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLog;

namespace Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;

        public UsersController(IUserService users)
        {
            _users = users;
        }

        [HttpPut("login")]
        public ActionResult<bool> Login([FromBody] JObject json)
        {
            var openId = (string)json["openId"];
            var nickName = (string)json["nickName"];
            var imageURL = (string)json["imageURL"];

            Logger log = LogManager.GetCurrentClassLogger();
            log.Info("openId = {0}, nickName = {1}, imageURL = {2}", openId, nickName, imageURL);

            if (_users.FindUser(openId))
                _users.UpdateUser(openId, nickName, imageURL);
            else _users.AddUser(openId, nickName, imageURL);

            return true;
        }
    }
}
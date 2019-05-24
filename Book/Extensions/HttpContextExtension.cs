using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Extensions
{
    public static class HttpContextExtension
    {
        private static readonly string UserKey = "UserKey";

        public static void SetUserKey(this HttpContext context, int userId)
        {
            context.Session.SetInt32(UserKey, userId);
        }

        public static int GetUserId(this HttpContext context)
        {
            var res = context.Session.GetInt32(UserKey);
            return res ?? throw new Exception("User not exists.");
        }
    }
}

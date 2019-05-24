using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class ResultModel
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public Object Data { get; set; }

        public static ResultModel Success()
        {
            return new ResultModel
            {
                Code = 0,
                Message = "",
                Data = null
            };
        }

        public static ResultModel Success(Object data)
        {
            return new ResultModel
            {
                Code = 0,
                Message = "",
                Data = data
            };
        }

        public static ResultModel Fail(string message)
        {
            return new ResultModel
            {
                Code = 1,
                Message = message,
                Data = null
            };
        }

        public static ResultModel Fail(string message, Object data)
        {
            return new ResultModel
            {
                Code = 1,
                Message = message,
                Data = data
            };
        }
    }
}

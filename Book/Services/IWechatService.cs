using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Services
{
    public interface IWechatService
    {
        JObject Code2Session(string code);
    }
}

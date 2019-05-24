using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Book.Services.Implementation
{
    public class WechatService : IWechatService
    {
        private readonly string _appId, _secret;

        public WechatService(string appId, string secret)
        {
            _appId = appId;
            _secret = secret;
        }

        public JObject Code2Session(string code)
        {
            string serviceURL = $"https://api.weixin.qq.com/sns/jscode2session?appid={_appId}&secret={_secret}&js_code={code}&grant_type=authorization_code";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceURL);
            request.Method = "GET";
            request.ContentType = "text/html;charset=utf-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string res = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8).ReadToEnd();
            return (JObject)JsonConvert.DeserializeObject(res);
        }
    }

    public static class WechatServiceExtensions
    {
        public static IServiceCollection AddWechatService(this IServiceCollection services,
            Func<IServiceProvider, WechatService> factory)
        {
            return services.AddSingleton<IWechatService, WechatService>(factory);
        }
    }
}

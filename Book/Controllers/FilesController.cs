using System;
using System.Collections.Generic;
using Book.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Book.Extensions;
using Microsoft.AspNetCore.Cors;

namespace Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class FilesController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _env;

        public FilesController(IHttpContextAccessor accessor, IHostingEnvironment env)
        {
            _accessor = accessor;
            _env = env;
        }

        [HttpPost("upload")]
        public ActionResult<ResultModel> PostFile()
        {
            try
            {
                var userId = _accessor.HttpContext.GetUserId();

                string filePath = Path.Combine(_env.WebRootPath, "Files");

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var res = new List<string>();

                var files = Request.Form.Files;
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    var fullPath = Path.Combine(filePath, fileName);
                    res.Add("/image/" + fileName);

                    using (var fs = System.IO.File.Create(fullPath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
                return ResultModel.Success(res);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return ResultModel.Fail(e.Message);
            }
        }
    }
}
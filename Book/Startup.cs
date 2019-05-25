using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Book.Data.Context;
using Book.Services;
using Book.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Book
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins(Environment.
                    GetEnvironmentVariable("ASPNETCORE_URLS")).
                    AllowAnyHeader().
                    AllowAnyMethod());
            });

            string appId = Configuration.GetSection("Wechat").GetValue<string>("AppId", null);
            string secret = Configuration.GetSection("Wechat").GetValue<string>("Secret");
            services.AddWechatService(_ => new WechatService(appId, secret));

            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMySqlContext(Configuration.GetConnectionString("Default"));
            services.AddUserService();
            services.AddSellService();
            services.AddRewardService();
            services.AddOrderService();
            services.AddShoppingCartService();
            services.AddPostService();
            services.AddCommentService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseCors("AllowSpecificOrigin");
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\\Files")),
                RequestPath = new PathString("/image")
            });
        }
    }
}

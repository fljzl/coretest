using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wikifx.BlockChain.Core;
using Wikifx.BlockChain.Core.Service;

namespace Wikifx.BlockChain.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;//不需要检查cookie
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).SetCompatibilityVersion(CompatibilityVersion.Latest).ConfigureApiBehaviorOptions(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            services.AddControllersWithViews(
                option =>
                {
                    // option.Filters.Add<GlobalExceptionFilter>();//全局错误处理
                })
                .AddRazorRuntimeCompilation();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            services.AddHttpClient();
            services.AddHttpClient<HeadService>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
         
            builder.RegisterModule(new AutoHelper());
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            //初始化请求上下文；
            MvcContext.Accessor = accessor;
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //处理500的错误
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStatusCodePages();//400
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?state={0}");



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default1",
                    template: "{country}_{language}",
                     defaults: new { controller = "Market", Action = "Market" }
                    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Wikifx.BlockChain.Core;
using Wikifx.BlockChain.Web.Models;

namespace Wikifx.BlockChain.Web.Controllers
{
    public class BaseController : Controller
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //var flag = true;
            //if (flag)
            //{
            //    return Task.Run(() =>
            //     {
            //         MvcContext.GetContext().Response.Redirect("http://www.baidu.com");
            //     });
            //}
            //else
            //{

            //    return base.OnActionExecutionAsync(context, next);
            //}

            return base.OnActionExecutionAsync(context, next);
        }
    }
}

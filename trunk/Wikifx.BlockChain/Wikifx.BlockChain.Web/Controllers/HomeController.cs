using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Wikifx.BlockChain.Core.Service;
using Wikifx.BlockChain.Core.ViewModel;
using Wikifx.BlockChain.Web.Models;

namespace Wikifx.BlockChain.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HeadService _webhelper { get; set; }
        public HomeController(HeadService webHelper)
        {
            _webhelper = webHelper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _webhelper.GetDate<MarketType>();
            Console.WriteLine(result);

            var result2 = await _webhelper.GetCache<MarketType>();
            Console.WriteLine(result2);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

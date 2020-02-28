using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontEnd.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            this.config = config;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PersonModel model)
        {
            if(ModelState.IsValid)
            {
                string output = JsonConvert.SerializeObject(model);
                HttpContent content = new StringContent(output, Encoding.UTF8, "application/json");
                string baseUrl = config["BackendBaseUrl"];
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    var result = await client.PostAsync("api/person", content);

                    if (result.IsSuccessStatusCode)
                        return View("Success");
                   
                }
            }

            return Error();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

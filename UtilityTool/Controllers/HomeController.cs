using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UtilityTool.DataBase.Models;
using UtilityTool.Models;

namespace UtilityTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly UtilityToolContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UtilityToolContext dbContext, ILogger<HomeController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
            string result = null;
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return View();
        }

        public IActionResult Try()
        {
            //using (StackExchange.Profiling.MiniProfiler.Current.("RandomBegins"))
            //{

            //}
            var tryYourLuck = _dbContext.TryYourLuck.Select(x => x).OrderBy(x => x.UpdateDateTime).FirstOrDefault();
            var randomNum = tryYourLuck == null ? GiveMeANumber(null) : GiveMeANumber(tryYourLuck.Numbers);
            var newNum = tryYourLuck != null ? tryYourLuck.Numbers + "," + randomNum : Convert.ToString(randomNum);
            if (tryYourLuck == null)
                insertRecord();
            else
                updateRecord(tryYourLuck);

            bool insertRecord()
            {
                tryYourLuck = new TryYourLuck() { Numbers = newNum, RemainingCount = 99, UpdateDateTime = DateTime.Now };
                _dbContext.TryYourLuck.Add(tryYourLuck);
                _dbContext.SaveChanges();
                return true;
            }

            bool updateRecord(TryYourLuck tryYourLuck)
            {
                tryYourLuck.Numbers = newNum;
                tryYourLuck.RemainingCount -= 1;
                tryYourLuck.UpdateDateTime = DateTime.Now;
                _dbContext.TryYourLuck.Update(tryYourLuck);
                _dbContext.SaveChanges();
                return true;
            }
            return View("Index", newNum);
        }

        private int GiveMeANumber(string excludeNumbers)
        {
            var num = excludeNumbers?.Split(',').Select(int.Parse).ToHashSet();
            var exclude = new HashSet<int>();
            if (num == null)
            {
                num = new HashSet<int>() { 0 };
            }
            exclude = num;
            var range = Enumerable.Range(0, 100).Where(i => !exclude.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, 100 - exclude.Count);
            return range.ElementAt(index);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C3SS_Web_Main.Models;
using C3SS_Web_Main.Classes;

namespace C3SS_Web_Main.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var blankModel = new SearchResultModel();
            blankModel.Success = false;
            return View(blankModel);
        }

        [HttpPost]
        public IActionResult Search(string searchName)
        {
            
            //would be preferable to make this using DI model, for times sake GitRepository will be directly accessed
            var gitRepos = new GitRepository();
            var response = gitRepos.SearchForUser(searchName);

            return Json(response);
        }
  

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

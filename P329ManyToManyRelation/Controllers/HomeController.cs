using Microsoft.AspNetCore.Mvc;
using P329ManyToManyRelation.Models;
using System.Diagnostics;

namespace P329ManyToManyRelation.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
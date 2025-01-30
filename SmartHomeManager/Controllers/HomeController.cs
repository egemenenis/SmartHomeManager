using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Models;
using System.Diagnostics;

namespace SmartHomeManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(int? statusCode)
        {
            var viewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = statusCode
            };

            return View(viewModel);
        }

        public IActionResult LearnMore()
        {
            return View();
        }
    }
}
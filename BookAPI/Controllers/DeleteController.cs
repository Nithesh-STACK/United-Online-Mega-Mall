using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    public class DeleteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

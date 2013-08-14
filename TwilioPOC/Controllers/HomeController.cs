using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwilioPOC.Data;
using TwilioPOC.Models;

namespace TwilioPOC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Feedback item)
        {
            if (!ModelState.IsValid)
                return Index();

            DataStore.Instance.Create(item);
            return Redirect("Review");
        }
    }
}

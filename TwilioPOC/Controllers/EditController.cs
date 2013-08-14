using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwilioPOC.Data;
using TwilioPOC.Models;

namespace TwilioPOC.Controllers
{
    public class EditController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Feedback item)
        {
            DataStore.Instance.ChangeStatus(item.Id, item.Status);
            return RedirectToAction("Index");
        }
    }
}

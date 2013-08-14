using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwilioPOC.Data;

namespace TwilioPOC.Controllers
{
    public class ReviewController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetFeedbackItems()
        {
            var items = DataStore.Instance.GetItems();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}

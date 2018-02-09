using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthAdmin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        #region PartialResults

        public PartialViewResult TopHeaderPartialResult()
        {
            return PartialView("_TopHeaderPartial");
        }

        public PartialViewResult SideMenuPartialResult()
        {
            return PartialView("_SideMenuPartial");
        }
        public PartialViewResult DrowdownMessagesPartialResult()
        {
            return PartialView("_DropdownMessagesPartial");
        }
        public PartialViewResult DrowdownTaskPartialResult()
        {
            return PartialView("_DropdownTaskPartial");
        }
        public PartialViewResult DrowdownAlertsPartialResult()
        {
            return PartialView("_DropdownAlertsPartial");
        }
        public PartialViewResult DrowdownUserPartialResult()
        {
            return PartialView("_DrowdownUserPartial");
        }
        #endregion
    }
}
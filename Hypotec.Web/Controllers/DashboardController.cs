using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hypotec.Web.Controllers
{
    public class DashboardController : Controller
    {
        // GET: DashboardController
        /// <summary>
        /// method of Dashboard page
        /// </summary>
        /// <returns></returns>
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}

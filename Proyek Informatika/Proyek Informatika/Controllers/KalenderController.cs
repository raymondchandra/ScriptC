using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers
{
    public class KalenderController : Controller
    {
        //
        // GET: /Kalender/

        public ActionResult Index()
        {
            return PartialView();
        }

    }
}

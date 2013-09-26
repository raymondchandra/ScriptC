using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers.Koordinator
{
    public class NilaiController : Controller
    {
        //
        // GET: /Nilai/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NilaiReport()
        {
            return PartialView();
        }
    }
}

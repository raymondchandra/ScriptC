using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers.Koordinator
{
    public class BimbinganKoordinatorController : Controller
    {
        //
        // GET: /Bimbingan/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bimbingan()
        {
            return PartialView();
        }

    }
}

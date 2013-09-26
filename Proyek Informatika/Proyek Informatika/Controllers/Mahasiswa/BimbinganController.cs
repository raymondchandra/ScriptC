using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers.Mahasiswa
{
    public class BimbinganController : Controller
    {
        //
        // GET: /Bimbingan/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PemesananIndex()
        {
            return PartialView();
        }

        public ActionResult PemesananKalender()
        {
            return PartialView();
        }

        public ActionResult PemesananSummary()
        {
            return PartialView();
        }

        public ActionResult KartuBimbingan()
        {
            return PartialView();
        }
    }
}

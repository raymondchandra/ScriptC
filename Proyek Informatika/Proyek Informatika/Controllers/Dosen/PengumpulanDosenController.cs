using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class PengumpulanDosenController : Controller
    {
        //
        // GET: /Pengumpulan/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PengumpulanFile()
        {
            return PartialView();
        }

    }
}

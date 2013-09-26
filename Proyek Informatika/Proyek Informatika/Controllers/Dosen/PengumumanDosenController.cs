using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class PengumumanDosenController : Controller
    {
        //
        // GET: /Pengumuman/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pengumuman()
        {
            return PartialView();
        }

        public ActionResult BuatPengumuman()
        {
            return PartialView();
        }

        public ActionResult EditPengumuman()
        {
            return PartialView();
        }
    }
}

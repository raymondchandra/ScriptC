using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers.Mahasiswa
{
    public class SidangController : Controller
    {
        //
        // GET: /Sidang/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Pengumpulan()
        {
            return PartialView();
        }

        public ActionResult Lihat()
        {
            return PartialView();
        }
    }
}

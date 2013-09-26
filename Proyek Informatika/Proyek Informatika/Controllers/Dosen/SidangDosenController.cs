using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class SidangDosenController : Controller
    {
        //
        // GET: /Sidang/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Penilaian()
        {
            return PartialView();
        }

        public ActionResult Sidang(string role)
        {
            ViewBag.role = role;
            return PartialView();
        }
    }
}

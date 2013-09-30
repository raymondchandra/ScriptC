using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class BimbinganDosenController : Controller
    {
        //
        // GET: /Bimbingan/

        public ActionResult Index()
        {
            return View();
            return PartialView();
        }

        public ActionResult JadwalBimbingan()
        {
            return PartialView();
        }
        public ActionResult KartuBimbingan()
        {
            return PartialView();
        }

    }
}

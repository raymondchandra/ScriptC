using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class ProfileDosenController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Biodata()
        {
            return PartialView();
        }

        public ActionResult Sejarah()
        {
            return PartialView();
        }


        public ActionResult Pengaturan()
        {
            return PartialView();
        }
    }
}

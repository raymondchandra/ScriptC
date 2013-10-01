using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

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
        protected ViewResult bindingPenilaianSidang(int id)
        {

            List<PenilaianSidang> temp = new List<PenilaianSidang>();

            PenilaianSidang x = new PenilaianSidang()
            {
                id = 2010730089,
                mahasiswa = "Albertus Alvin",
                pembimbing = "Lionov",
                penguji1 = "Oerip S. Santoso",
                penguji2 = "-",
            };
            temp.Add(x);
            x = new PenilaianSidang()
            {
                id = 2010730125,
                mahasiswa = "Hans Wirya Halim",
                pembimbing = "Ceacilia Nugraheni",
                penguji1 = "Lionov",
                penguji2 = "-",
            };
            temp.Add(x);
            x = new PenilaianSidang()
            {
                id = 2011730014,
                mahasiswa = "Ariyandi Widarto",
                pembimbing = "Gede Karya",
                penguji1 = "Elisati Hulu",
                penguji2 = "Lionov",
            };
            temp.Add(x);
            return View(new GridModel<PenilaianSidang>
            {
                Data = temp
            });
        }
        [GridAction]
        public ActionResult _SelectPenilaianSidang()
        {
            return bindingPenilaianSidang(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SavePenilaianSidang(int id)
        {

            return bindingPenilaianSidang(id);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertPenilaianSidang()
        {

            return bindingPenilaianSidang(2);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeletePenilaianSidang(int id)
        {

            return bindingPenilaianSidang(id);
        }

    }
}

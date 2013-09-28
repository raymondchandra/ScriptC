using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;


namespace Proyek_Informatika.Controllers.Koordinator
{
    public class NilaiController : Controller
    {
        //
        // GET: /Nilai/

        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult NilaiReport()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult _SelectNilaiSkripsi1()
        {
            return bindingNilaiSkripsi1(1);
        }
        protected ViewResult bindingNilaiSkripsi1(int id)
        {
            List<KoordinatorPenilaian> temp;

            temp = new List<KoordinatorPenilaian>();

            temp.Add(new KoordinatorPenilaian()
            {
                id = 1,
                npm = "2010730069",
                nama = "Cemon",
                judul = "Graph",
                nilaiAkhir = 90
            });
            return View(new GridModel<KoordinatorPenilaian>() { Data = temp });
        }
        public ActionResult NilaiSkripsi1()
        {
            return PartialView();
        }

        public ActionResult NilaiSkripsi2()
        {
            return PartialView();
        }
        public ActionResult DetailNilaiSkripsi1()
        {
            return PartialView();
        }

        public ActionResult DetailNilaiSkripsi2()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult _SelectNilaiSkripsi2()
        {
            return bindingNilaiSkripsi2(1);
        }

        protected ViewResult bindingNilaiSkripsi2(int id)
        {
            List<KoordinatorPenilaian> temp;

            temp = new List<KoordinatorPenilaian>();
            temp.Add(new KoordinatorPenilaian()
            {
                id = 2,
                npm = "2010730089",
                nama = "Ari Stop Hanes",
                judul = "Arima",
                nilaiAkhir = 86
            });
            return View(new GridModel<KoordinatorPenilaian>() { Data = temp });
        }
        [GridAction]
        public ViewResult _SelectDetailSkripsi2(int id)
        {
            List<KoordinatorPenilaianSkripsi2> temp;

            temp = new List<KoordinatorPenilaianSkripsi2>();

            temp.Add(new KoordinatorPenilaianSkripsi2()
            {
                id = 1,
                penilai = "Thomas Anung",
                komponen1 = 20,
                komponen2 = 30,
                komponen3 = 50,
                nilai1 = 70,
                nilai2 = 80,
                nilai3 = 90
            });
            temp.Add(new KoordinatorPenilaianSkripsi2()
            {
                id = 2,
                penilai = "Luciana A.",
                komponen1 = 40,
                komponen2 = 20,
                komponen3 = 40,
                nilai1 = 60,
                nilai2 = 85,
                nilai3 = 87
            });
            temp.Add(new KoordinatorPenilaianSkripsi2()
            {
                id = 2,
                penilai = "Oerip S.",
                komponen1 = 35,
                komponen2 = 35,
                komponen3 = 30,
                nilai1 = 62,
                nilai2 = 87,
                nilai3 = 86
            });
            return View(new GridModel<KoordinatorPenilaianSkripsi2>() { Data = temp });
        }

        public ActionResult NilaiPembimbing()
        {
            return PartialView();
        }
        public ActionResult NilaiPenguji1()
        {
            return PartialView();
        }
        public ActionResult NilaiPenguji2()
        {
            return PartialView();
        }
    }
}

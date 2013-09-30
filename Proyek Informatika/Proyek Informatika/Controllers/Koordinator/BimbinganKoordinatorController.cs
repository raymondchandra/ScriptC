using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;
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

        [GridAction]
        public ActionResult SelectBimbingan()
        {
            return bindingBimbingan(10);
        }

        protected ViewResult bindingBimbingan(int id)
        {
            List<KoordinatorBimbingan> temp;

            temp = new List<KoordinatorBimbingan>();
            temp.Add(new KoordinatorBimbingan() {
                id = 10,
                judul="JST",
                jumlahBimbingan = 2, 
                namaDosen= "Lionov",
                namaMahasiswa ="Remon",
                nikDosen="1000",
                npmMahasiswa="2010730069",
                periode="Ganjil 2013/2014",
                tipe ="Skripsi-1"
            });

            return View(new GridModel<KoordinatorBimbingan> { Data = temp});
        }
        public ActionResult Skripsi1()
        {
            return PartialView();
        }

        public ActionResult Skripsi2()
        {
            return PartialView();
        }
    }
}

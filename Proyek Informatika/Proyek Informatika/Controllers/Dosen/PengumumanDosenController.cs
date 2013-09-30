using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class PengumumanDosenController : Controller
    {
        //
        // GET: /BlokPengumuman/

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

        public ActionResult EditPengumumanWindow()
        {
            return PartialView();
        }

        protected ViewResult bindingPengumumanDosen(int id)
        {

            List<PengumumanDosen> temp = new List<PengumumanDosen>();

            PengumumanDosen x = new PengumumanDosen()
            {
                id = 0,
                Tanggal = "Kamis, 11 Juli 2013 11:00",
                Judul = "Bimbingan Minggu ini Ditiadakan",
                Isi = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed malesuada magna neque quis accumsan tellus euismod et. Nulla sollicitudin, magna ac lobortis venenatis, orci lacus pulvinar arcu, id auctor sapien lectus ac metus. Nulla porttitor imperdiet augue ut ornare. Praesent a imperdiet nisi, vel suscipit orci. Nullam pulvinar sem dui, porta vehicula augue accumsan sed. Nulla facilisi. Etiam tincidunt, tortor eget sagittis tempus, eros nisi eleifend urna, nec auctor dui dolor in orci. Fusce adipiscing lectus ac imperdiet dapibus. Etiam ipsum arcu, elementum a risus ac,laoreet faucibus dolor.",
                Penulis = "Verliyantina",
                Target = "Mahasiswa Skripsi 2",
            };
            temp.Add(x);

            return View(new GridModel<PengumumanDosen>
            {
                Data = temp
            });
        }
        [GridAction]
        public ActionResult _SelectPengumumanDosen()
        {
            return bindingPengumumanDosen(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SavePengumumanDosen(int id)
        {

            return bindingPengumumanDosen(id);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertPengumumanDosen()
        {

            return bindingPengumumanDosen(2);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeletePengumumanDosen(int id)
        {

            return bindingPengumumanDosen(id);
        }

    }
}

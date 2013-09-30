using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

namespace Proyek_Informatika.Controllers.Koordinator
{
    public class PengumumanKoordinatorController : Controller
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

        public ActionResult EditPengumumanWindow()
        {
            return PartialView();
        }

        protected ViewResult bindingPengumumanKoordinator(int id)
        {

            List<PengumumanKoordinator> temp = new List<PengumumanKoordinator>();

            PengumumanKoordinator x = new PengumumanKoordinator()
            {
                id = 0,
                Tanggal = "Kamis, 11 Juli 2013 11:00",
                Judul = "Bimbingan Minggu ini Ditiadakan",
                Isi = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed malesuada magna neque quis accumsan tellus euismod et. Nulla sollicitudin, magna ac lobortis venenatis, orci lacus pulvinar arcu, id auctor sapien lectus ac metus. Nulla porttitor imperdiet augue ut ornare. Praesent a imperdiet nisi, vel suscipit orci. Nullam pulvinar sem dui, porta vehicula augue accumsan sed. Nulla facilisi. Etiam tincidunt, tortor eget sagittis tempus, eros nisi eleifend urna, nec auctor dui dolor in orci. Fusce adipiscing lectus ac imperdiet dapibus. Etiam ipsum arcu, elementum a risus ac,laoreet faucibus dolor.",
                Penulis = "Verliyantina",
                Target = "Mahasiswa Skripsi 2",
            };
            temp.Add(x);

            x = new PengumumanKoordinator()
            {
                id = 1,
                Tanggal = "Senin, 23 September 2013 14:00",
                Judul = "Pengumuman bagi para Dosen Pembimbing",
                Isi = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed malesuada magna neque quis accumsan tellus euismod et. Nulla sollicitudin, magna ac lobortis venenatis, orci lacus pulvinar arcu, id auctor sapien lectus ac metus. Nulla porttitor imperdiet augue ut ornare. Praesent a imperdiet nisi, vel suscipit orci. Nullam pulvinar sem dui, porta vehicula augue accumsan sed. Nulla facilisi. Etiam tincidunt, tortor eget sagittis tempus, eros nisi eleifend urna, nec auctor dui dolor in orci. Fusce adipiscing lectus ac imperdiet dapibus. Etiam ipsum arcu, elementum a risus ac,laoreet faucibus dolor.",
                Penulis = "Koordinator",
                Target = "Dosen Pembimbing",
            };
            temp.Add(x);


            x = new PengumumanKoordinator()
            {
                id = 2,
                Tanggal = "Selasa, 24 September 2013 9:25",
                Judul = "Pendaftaran Topik Skripsi",
                Isi = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed malesuada magna neque quis accumsan tellus euismod et. Nulla sollicitudin, magna ac lobortis venenatis, orci lacus pulvinar arcu, id auctor sapien lectus ac metus. Nulla porttitor imperdiet augue ut ornare. Praesent a imperdiet nisi, vel suscipit orci. Nullam pulvinar sem dui, porta vehicula augue accumsan sed. Nulla facilisi. Etiam tincidunt, tortor eget sagittis tempus, eros nisi eleifend urna, nec auctor dui dolor in orci. Fusce adipiscing lectus ac imperdiet dapibus. Etiam ipsum arcu, elementum a risus ac,laoreet faucibus dolor.",
                Penulis = "Koordinator",
                Target = "Mahasiswa Skripsi 1",
            };
            temp.Add(x);
            return View(new GridModel<PengumumanKoordinator>
            {
                Data = temp
            });
        }
        [GridAction]
        public ActionResult _SelectPengumumanKoordinator()
        {
            return bindingPengumumanKoordinator(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SavePengumumanKoordinator(int id)
        {

            return bindingPengumumanKoordinator(id);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertPengumumanKoordinator()
        {

            return bindingPengumumanKoordinator(2);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeletePengumumanKoordinator(int id)
        {

            return bindingPengumumanKoordinator(id);
        }

    }
}

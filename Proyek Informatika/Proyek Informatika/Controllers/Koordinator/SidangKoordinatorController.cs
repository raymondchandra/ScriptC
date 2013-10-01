using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;

namespace Proyek_Informatika.Controllers.Koordinator
{
    public class SidangKoordinatorController : Controller
    {
        //
        // GET: /Sidang/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PengaturanJadwal()
        {
            return PartialView();
        }

        public ActionResult PengaturanSidang()
        {
            return PartialView();
        }

        protected ViewResult bindingPembimbing(int id)
        {

            List<GradeCategory> temp = new List<GradeCategory>();
            
                GradeCategory x = new GradeCategory()
                {
                    tipe = "pembimbing",
                    bobot = "20",
                    kategori = "Tata tulis Laporan",
                    id = 5
                };
                temp.Add(x);
                 x = new GradeCategory()
                {
                    tipe = "pembimbing",
                    bobot = "20",
                    kategori = "Kelengkapan materi",
                    id = 5
                };
                temp.Add(x);
                x = new GradeCategory()
                {
                    tipe = "pembimbing",
                    bobot = "30",
                    kategori = "Penguasaan materi",
                    id = 5
                };
                temp.Add(x);
                x = new GradeCategory()
                {
                    tipe = "pembimbing",
                    bobot = "30",
                    kategori = "Proses Bimbingan",
                    id = 5
                };
                temp.Add(x);
            return View(new GridModel<GradeCategory>
            {
                Data = temp
            });
        }
        [GridAction]
        public ActionResult _SelectPembimbing()
        {
            return bindingPembimbing(5);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SavePembimbing(int id)
        {

            return bindingPembimbing(id);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertPembimbing()
        {

            return bindingPembimbing(1);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeletePembimbing(int id)
        {

            return bindingPembimbing(id);
        }

        protected ViewResult bindingPenguji(int id)
        {

            List<GradeCategory> temp = new List<GradeCategory>();

            GradeCategory x = new GradeCategory()
            {
                tipe = "pembimbing",
                bobot = "15",
                kategori = "Tata tulis Laporan",
                id = 5
            };
            temp.Add(x);
            x = new GradeCategory()
            {
                tipe = "pembimbing",
                bobot = "20",
                kategori = "Kelengkapan materi",
                id = 5
            };
            temp.Add(x);
            x = new GradeCategory()
            {
                tipe = "pembimbing",
                bobot = "30",
                kategori = "Penguasaan materi",
                id = 5
            };
            temp.Add(x);
            x = new GradeCategory()
            {
                tipe = "pembimbing",
                bobot = "20",
                kategori = "Pemahaman",
                id = 5
            };
            temp.Add(x);
            return View(new GridModel<GradeCategory>
            {
                Data = temp
            });
        }
        [GridAction]
        public ActionResult _SelectPenguji()
        {
            return bindingPenguji(1);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SavePenguji(int id)
        {

            return bindingPenguji(id);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertPenguji()
        {

            return bindingPenguji(2);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeletePenguji(int id)
        {

            return bindingPenguji(id);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

namespace Proyek_Informatika.Controllers.Mahasiswa
{
    public class ProfileController : Controller
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
        protected ViewResult bindingSejarahMahasiswa(int id)
        {

            List<SejarahMahasiswa> temp = new List<SejarahMahasiswa>();

            SejarahMahasiswa x = new SejarahMahasiswa()
            {
                id = 0,
                MataKuliah = "Skripsi 2",
                Tahun = 2013,
                Semester = "genap",
                Topik = "Aplikasi Mobile dengan Pendekatan MVC Terintegrasi dengan Server dan Web Service",
                Pembimbing = "Gede Karya",
                Keterangan = "ganti topik"
            }; temp.Add(x);
            x = new SejarahMahasiswa()
            {
                id = 1,
                MataKuliah = "Skripsi 1",
                Tahun = 2012,
                Semester = "ganjil",
                Topik = "Aplikasi Mobile dengan Pendekatan MVC Terintegrasi dengan Server dan Web Service",
                Pembimbing = "Gede Karya",
                Keterangan = "lulus"
            }; temp.Add(x);


            return View(new GridModel<SejarahMahasiswa>
            {
                Data = temp
            });
        }
        [GridAction]
        public ActionResult _SelectSejarahMahasiswa()
        {
            return bindingSejarahMahasiswa(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveSejarahMahasiswa(int id)
        {

            return bindingSejarahMahasiswa(id);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertSejarahMahasiswa()
        {

            return bindingSejarahMahasiswa(2);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteSejarahMahasiswa(int id)
        {

            return bindingSejarahMahasiswa(id);
        }

    }
}
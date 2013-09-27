using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

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
        protected ViewResult bindingSejarahDosen(int id)
        {

            List<SejarahDosen> temp = new List<SejarahDosen>();

            SejarahDosen x = new SejarahDosen()
            {
                id = 0,
                MataKuliah = "Skripsi 2",
                Tahun = 2004,
                Semester = "ganjil",
                Topik = "Pembangunan Perangkat Lunak Sistem Akuntansi Pembukuan dengan Pendekatan Berorientasi Obyek",
                NPM = 1998730039,
                Mahasiswa = "Elisa Dean",
                Keterangan = "lulus"
            }; temp.Add(x);

            x = new SejarahDosen()
            {
                id = 1,
                MataKuliah = "Skripsi 1",
                Tahun = 2003,
                Semester = "genap",
                Topik = "Pembangunan Perangkat Lunak Sistem Akuntansi Pembukuan dengan Pendekatan Berorientasi Obyek",
                NPM = 1998730039,
                Mahasiswa = "Elisa Dean",
                Keterangan = "lulus"
            }; temp.Add(x);

            x = new SejarahDosen()
            {
                id = 2,
                MataKuliah = "Skripsi 2",
                Tahun = 2003,
                Semester = "ganjil",
                Topik = "Perangkat Lunak Pendukung Sistem Evaluasi Diri Program Studi",
                NPM = 1998730025,
                Mahasiswa = "Aryanugroho Wargono",
                Keterangan = "lulus"
            }; temp.Add(x);


            x = new SejarahDosen()
            {
                id = 3,
                MataKuliah = "Skripsi 1",
                Tahun = 2003,
                Semester = "genap",
                Topik = "Perangkat Lunak Pendukung Sistem Evaluasi Diri Program Studi",
                NPM = 1998730025,
                Mahasiswa = "Aryanugroho Wargono",
                Keterangan = "lulus"
            }; temp.Add(x);
            return View(new GridModel<SejarahDosen>
            {
                Data = temp
            });
        }
        [GridAction]
        public ActionResult _SelectSejarahDosen()
        {
            return bindingSejarahDosen(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveSejarahDosen(int id)
        {

            return bindingSejarahDosen(id);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertSejarahDosen()
        {

            return bindingSejarahDosen(2);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteSejaraDosen(int id)
        {

            return bindingSejarahDosen(id);
        }

    }
}
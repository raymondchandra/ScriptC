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
        SkripsiAutoContainer db = new SkripsiAutoContainer();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bimbingan()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult SelectBimbingan(int tipe)
        {
            return bindingBimbingan(tipe);
        }

        protected ViewResult bindingBimbingan(int tipe)
        {
            var result = from si in db.skripsis
                                                where si.jenis == tipe
                                                select new KoordinatorBimbingan() {
                                                    id= si.id,
                                                    judul = si.topik.judul,
                                                    jumlahBimbingan = 0,
                                                    namaDosen = si.dosen.nama,
                                                    namaMahasiswa = si.mahasiswa.nama,
                                                    nikDosen = si.dosen.NIK,
                                                    npmMahasiswa = si.mahasiswa.NPM,
                                                    periode = si.semester.periode_semester
                                                    };
            List<KoordinatorBimbingan> temp = new List<KoordinatorBimbingan>();

            foreach(var x in result)
            {
                KoordinatorBimbingan temp2 = x;
                temp2.jumlahBimbingan = db.bimbingans.Where<bimbingan>(a => a.id_skripsi == temp2.id).Count<bimbingan>();
                temp.Add(temp2);
            }

            return View(new GridModel<KoordinatorBimbingan> { Data = temp});
        }
        #region kartubimbingan
        public ActionResult KartuBimbingan(int id_skripsi)
        {
            var result = (from sk in db.skripsis
                          join mah in db.mahasiswas on sk.NPM_mahasiswa equals mah.NPM
                          join tp in db.topiks on sk.id_topik equals tp.id
                          where (sk.id == id_skripsi)
                          select new DosenMuridBimbinganContainer { id = sk.id, judul = tp.judul, namaMahasiswa = mah.nama, npm = mah.NPM }).Single<DosenMuridBimbinganContainer>();
            int count = db.bimbingans.Where(x => x.id_skripsi == id_skripsi).Count();
            ViewBag.jumlahBimbingan = count;
            ViewBag.nama = result.namaMahasiswa;
            ViewBag.npm = result.npm;
            ViewBag.judul = result.judul;
            ViewBag.id = result.id;
            return PartialView();
        }
        


        #endregion
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

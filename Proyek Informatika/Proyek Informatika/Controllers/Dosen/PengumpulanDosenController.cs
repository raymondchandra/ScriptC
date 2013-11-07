using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
namespace Proyek_Informatika.Controllers.Dosen
{
    public class PengumpulanDosenController : Controller
    {
        //
        // GET: /Pengumpulan/
        SkripsiAutoContainer db = new SkripsiAutoContainer();
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult FileMahasiswa(int id_skripsi)
        {
            var result = (from sk in db.skripsis
                          join mah in db.mahasiswas on sk.NPM_mahasiswa equals mah.NPM
                          join tp in db.topiks on sk.id_topik equals tp.id
                          where (sk.id == id_skripsi)
                          select new DosenMuridBimbinganContainer { id = sk.id, judul = tp.judul, namaMahasiswa = mah.nama, npm = mah.NPM }).Single<DosenMuridBimbinganContainer>();
            ViewBag.nama = result.namaMahasiswa;
            ViewBag.npm = result.npm;
            ViewBag.judul = result.judul;
            ViewBag.id = result.id;
            return PartialView();
        }
        public ActionResult PengumpulanFile()
        {
            return PartialView();
        }

        public ActionResult IndexPengumpulan()
        {
            return PartialView();
        }

        public ActionResult Skripsi1()
        {
            return PartialView();
        }
        public ActionResult Skripsi2()
        {
            return PartialView();
        }
        public ActionResult PreviewDocument()
        {
            return PartialView();
        }
    }
}

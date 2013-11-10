using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;
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
        [GridAction]
        public ActionResult SelectPengumpulan(int id_skripsi)
        {
            
            return bindingPengumpulan(id_skripsi);

        }
        public ViewResult bindingPengumpulan(int id_skripsi)
        {
            var result = db.laporans.Where<laporan>(x => x.id_skripsi == id_skripsi);

            List<laporan> temp = new List<laporan>();

            foreach (var i in result)
            {
                laporan temp2 = new laporan();
                temp2.id = i.id;
                temp2.deskripsi = i.deskripsi;
                temp2.jenis = i.jenis;
                temp2.id_skripsi = i.id_skripsi;
                temp.Add(temp2);
            }

            return View(new GridModel<laporan> { Data = temp });
        }
        public ActionResult PengumpulanFile()
        {
            return PartialView();
        }

        public ActionResult IndexPengumpulan()
        {
            return PartialView();
        }

    }
}

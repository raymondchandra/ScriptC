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
            return PartialView();
        }
        public ActionResult IndexMahasiswa()
        {
            EnumSemesterSkripsiContainer semes = new EnumSemesterSkripsiContainer();
            var result = from jn in db.jenis_skripsi
                         select (new { id = jn.id, nama_jenis = jn.nama_jenis });
            List<jenis_skripsi> temp = new List<jenis_skripsi>();
            foreach (var x in result)
            {
                temp.Add(new jenis_skripsi { id = x.id, nama_jenis = x.nama_jenis });
            }
            semes.jenis_skripsi = temp;

            List<semester> temp2 = new List<semester>();
            var result2 = from si in db.semesters
                          select (new { id = si.id, nama = si.periode_semester, curr = si.isCurrent });
            foreach (var x in result2)
            {
                temp2.Add(new semester { id = x.id, periode_semester = x.nama, isCurrent = x.curr });
                if (x.curr == 1)
                {
                    semes.selected_value = x.id;
                }
            }
            semes.jenis_semester = temp2;
            return PartialView(semes);
        }
        
        public ActionResult Report()
        {
            return PartialView();
        }
        
        [HttpPost]
        public ActionResult ListMahasiswa(int periode=0, int jenis_skripsi=0)
        {
            ViewBag.periode = periode;
            ViewBag.jenis_skripsi = jenis_skripsi;
            return PartialView();
        }

        [GridAction]
        public ActionResult SelectBimbingan(int periode, int jenis_skripsi)
        {
            return bindingBimbingan(periode, jenis_skripsi);
        }

        protected ViewResult bindingBimbingan(int periode, int jenis_skripsi)
        {
            var result = from si in db.skripsis
                         where si.jenis == jenis_skripsi && si.id_semester_pengambilan == periode
                         select new KoordinatorListMahasiswa() {
                             id= si.id,
                             judul = si.topik.judul,
                             jumlahBimbingan = 0,
                             namaDosen = si.dosen.nama,
                             namaMahasiswa = si.mahasiswa.nama,
                             nikDosen = si.dosen.NIK,
                             npmMahasiswa = si.mahasiswa.NPM,
                             periode = si.semester.periode_semester,
                             pengambilanke = si.pengambilan_ke,
                             tipe = si.jenis_skripsi.nama_jenis
                             };
            List<KoordinatorListMahasiswa> temp = new List<KoordinatorListMahasiswa>();

            foreach(var x in result)
            {
                KoordinatorListMahasiswa temp2 = x;
                temp2.jumlahBimbingan = db.bimbingans.Where<bimbingan>(a => a.id_skripsi == temp2.id).Count<bimbingan>();
                temp.Add(temp2);
            }

            return View(new GridModel<KoordinatorListMahasiswa> { Data = temp});
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

    }
}

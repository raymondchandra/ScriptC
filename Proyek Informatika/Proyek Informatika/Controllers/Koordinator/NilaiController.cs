using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;


namespace Proyek_Informatika.Controllers.Koordinator
{
    public class NilaiController : Controller
    {
        //
        // GET: /Nilai/
        SkripsiAutoContainer db = new SkripsiAutoContainer();
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult Report()
        {
            return PartialView();
        }
        public ActionResult NilaiReport()
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
        [HttpPost]
        public ActionResult ListMahasiswa(int periode=0, int jenis_skripsi=0)
        {
            ViewBag.periode = periode;
            ViewBag.jenis_skripsi = jenis_skripsi;
            return PartialView();
        }

        [GridAction]
        public ActionResult SelectNilai(int periode, int jenis_skripsi)
        {
            return bindingNilai(periode, jenis_skripsi);
        }

        protected ViewResult bindingNilai(int periode, int jenis_skripsi)
        {
            var result = from si in db.nilais
                         where si.skripsi.jenis == jenis_skripsi && si.skripsi.id_semester_pengambilan == periode && si.kategori_nilai.kategori == "nilaiAkhir"
                         select new KoordinatorNilaiMahasiswa()
                         {
                             id = si.skripsi.id,
                             judul = si.skripsi.topik.judul,
                             namaDosen = si.skripsi.dosen.nama,
                             namaMahasiswa = si.skripsi.mahasiswa.nama,
                             nikDosen = si.skripsi.dosen.NIK,
                             npmMahasiswa = si.skripsi.mahasiswa.NPM,
                             periode = si.skripsi.semester.periode_semester,
                             pengambilanke = si.skripsi.pengambilan_ke,
                             tipe = si.skripsi.jenis_skripsi.nama_jenis,
                             nilai = si.angka
                         };
            List<KoordinatorNilaiMahasiswa> temp = result.ToList();

            foreach (var x in temp)
            {
                x.status = getStatusNilai(x.id);
            }

            return View(new GridModel<KoordinatorNilaiMahasiswa> { Data = temp });
        }

        public ActionResult DetailNilai(int id=0)
        {
            skripsi result = db.skripsis.Where(x => x.id == id).Single<skripsi>();

            if (result.jenis == 1)
            {
                return NilaiSkripsi1(id);
            }
            else
            {
                return NilaiSkripsi2(id);
            }
        }

        public ActionResult NilaiSkripsi1(int id=0)
        {
            var result = db.skripsis.Where(x => x.id == id).Single();

            ViewBag.id = id;
            ViewBag.NPM = result.NPM_mahasiswa;
            ViewBag.nama = result.mahasiswa.nama;
            ViewBag.judul = result.topik.judul;
            ViewBag.pembimbing = result.dosen.nama;

            var resultNilai = db.nilais.Where(x => x.id_skripsi == id && x.kategori_nilai.tipe == "general").OrderBy(x => x.kategori_nilai.urutan);

            List<Tuple<string, int, int, double>> listNilai = new List<Tuple<string, int, int, double>>();

            foreach (var item in resultNilai)
            {
                listNilai.Add(new Tuple<string, int, int, double>(item.kategori_nilai.kategori, item.kategori, item.kategori_nilai.bobot, item.angka));
            }
            
            ViewData["nilai"] = listNilai;

            var resultTotal = db.nilais.Where(x => x.id_skripsi == id && x.kategori_nilai.kategori == "nilaiAkhir").Single();
            ViewBag.total = resultTotal.angka;
            return PartialView();
        }
        [HttpPost]
        public ActionResult NilaiSkripsi2(int id)
        {
            
            ViewBag.id = id;
            return PartialView();
        }
        public ActionResult TabSkripsi2(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public int ChangeStatus(int id, byte status)
        {
            var result = db.nilais.Where(x => x.id_skripsi == id);

            foreach (var item in result)
            {
                item.submitted = status;
                if (item.kategori_nilai.tipe == "final" && status == 2)
                {
                    changeSkripsiTable(item.id_skripsi, item.angka);
                }
            }

            try
            {
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int ChangeAllStatus(int periode, int jenis, byte status)
        {
            var result = db.nilais.Where(x => x.skripsi.id_semester_pengambilan == periode && x.skripsi.jenis == jenis);

            foreach (var item in result)
            {
                item.submitted = status;
                if (item.kategori_nilai.tipe == "final" && status == 2)
                {
                    changeSkripsiTable(item.id_skripsi, item.angka);
                }
            }

            try
            {
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int ChangeAllSubmitedStatus(int periode, int jenis, byte status)
        {
            var result = db.nilais.Where(x => x.skripsi.id_semester_pengambilan == periode && x.skripsi.jenis == jenis && x.submitted == 1);

            foreach (var item in result)
            {
                item.submitted = status;
                if (item.kategori_nilai.tipe == "final" && status == 2)
                {
                    changeSkripsiTable(item.id_skripsi, item.angka);
                }
            }

            try
            {
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        private int changeSkripsiTable(int id, double angka)
        {
            var result = db.skripsis.Where(x => x.id == id).Single();
            if (angka > 80)
            {
                result.nilai_akhir = "A";
            }
            else if( angka > 70)
            {
                result.nilai_akhir = "B";
            }
            else if (angka > 60)
            {
                result.nilai_akhir = "C";
            }
            else
            {
                result.nilai_akhir = "E";
            }
            try
            {
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        private string getStatusNilai(int id)
        {
            byte countNilai = db.nilais.Where(x => x.id_skripsi == id && x.kategori_nilai.kategori.Equals( "nilaiAkhir")).SingleOrDefault().submitted;

            if (countNilai== 0)
            {
                return "Not Submitted";
            }
            else if (countNilai == 1)
            {
                return "Submited";
            }
            else
            {
                return "Finalized";
            }
        }

        private string getNik(int id)
        {
            string nik = db.skripsis.Where(x => x.id == id).Single().NIK_dosen_pembimbing;
            return nik;
        }

    }
}

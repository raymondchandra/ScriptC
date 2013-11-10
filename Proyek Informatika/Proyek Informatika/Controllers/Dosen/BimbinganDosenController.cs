using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
namespace Proyek_Informatika.Controllers.Dosen
{
    public class BimbinganDosenController : Controller
    {
        //
        // GET: /Bimbingan/
        SkripsiAutoContainer db = new SkripsiAutoContainer();
        #region  bimbingan

        public ActionResult Index()
        {
            HansContainer.EnumSemesterSkripsiContainer semes = new HansContainer.EnumSemesterSkripsiContainer();
            var result = from jn in db.jenis_skripsi 
                                  select (new {id = jn.id, nama_jenis =jn.nama_jenis });
            List<jenis_skripsi> temp = new List<jenis_skripsi>();
            foreach (var x in result)
            {
                temp.Add(new jenis_skripsi {id = x.id, nama_jenis = x.nama_jenis });
            }
            semes.jenis_skripsi = temp;

            List<semester> temp2 = new List<semester>();
            var result2 = from si in db.semesters
                     select (new {id = si.id, nama = si.periode_semester, curr = si.isCurrent });
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
        public ActionResult IndexBimbingan()
        {
            return PartialView();
        }
        public ActionResult MahasiswaBimbingan()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult MahasiswaBimbingan(int id_periode, int id_jenis_skripsi)
        {
            ViewBag.username = Session["username"].ToString();
            ViewBag.periode = id_periode;
            ViewBag.jenis_skripsi = id_jenis_skripsi;


            return PartialView();
        }
        [HttpPost]
        [GridAction]
        public ActionResult _SelectBimbinganMahasiswa(int periode, int jenis_skripsi)
        {
            
            string username = Session["username"].ToString();
            var sNIK = db.dosens.Where(x => x.username == username).Single<dosen>();
            string nNIK = sNIK.NIK;

            return bindingBimbinganMahasiswa(nNIK, periode, jenis_skripsi);
        }
       
        //binding list smua mahasiswa yang dibimbing
        public ViewResult bindingBimbinganMahasiswa(string nik,int periode, int jenis_skripsi)
        {
            
            var result = from sk in db.skripsis
                         where (sk.NIK_dosen_pembimbing == nik && sk.jenis == jenis_skripsi && sk.id_semester_pengambilan == periode)
                         select new DosenMuridBimbinganContainer{ id = sk.id, judul = sk.topik.judul, namaMahasiswa= sk.mahasiswa.nama, npm = sk.mahasiswa.NPM };
            List<DosenMuridBimbinganContainer> temp = result.ToList<DosenMuridBimbinganContainer>();

            //foreach (var i in result)
            //{
            //    temp.Add(new DosenMuridBimbinganContainer() { id = i.id, judul = i.judul, namaMahasiswa = i.nama, npm = i.NPM });
            //}
            return View(new GridModel<DosenMuridBimbinganContainer>() { Data = temp});

        }
        [HttpPost]
        public ActionResult FormBimbingan(int id_skripsi)
        {
            ViewBag.id_skripsi = id_skripsi;
            return PartialView();
        }
        [HttpPost]
        public ActionResult EditorBimbingan(int id_bimbingan)
        {
            ViewBag.id_bimbingan = id_bimbingan;
            bimbingan result = db.bimbingans.Where<bimbingan>(x => x.id == id_bimbingan).Single<bimbingan>();
            return PartialView(result);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult EditBimbingan(int id)
        {
            bimbingan result = db.bimbingans.Where<bimbingan>(x => x.id == id).Single<bimbingan>();
            TryUpdateModel<bimbingan>(result);
            int tipe = result.id_skripsi;
            db.SaveChanges();
            return bindingKartu(tipe);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult DeleteBimbingan(int id)
        {
            bimbingan result = db.bimbingans.Where<bimbingan>(x => x.id == id).Single<bimbingan>();
            
            int tipe = result.id_skripsi;
            db.bimbingans.Remove(result);
            db.SaveChanges();
            return bindingKartu(tipe);
        }
        
        [HttpPost]
        public void InsertBimbingans(int id_skripsi)
        {
            bimbingan bim = new bimbingan();
            bim.id_skripsi = id_skripsi;
            bim.tanggal = DateTime.Now;
            //Perform model binding (fill the product properties and validate it).
            bim.deskripsi = "";
            bim.isi = "";
            db.bimbingans.Add(bim);
            db.SaveChanges();
 
            //Rebind the grid
        }
        [HttpPost]
        public int InsertBimbingan(int id_skripsi, string judul, string deskripsi)
        {
            bimbingan bim = new bimbingan();
            bim.id_skripsi = id_skripsi;
            bim.isi = judul;
            bim.deskripsi = deskripsi;
            bim.tanggal = DateTime.Now;

            try
            {
                db.bimbingans.Add(bim);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        #endregion


        #region kartubimbingan

        [GridAction]
        public ActionResult _SelectKartu(int id_skripsi)
        {
            return bindingKartu(id_skripsi);
        }
        public ViewResult bindingKartu(int id_skripsi)
        {
            var result = from bm in db.bimbingans
                         where bm.id_skripsi == id_skripsi
                         select new { id = bm.id, id_skripsi = bm.id_skripsi, isi = bm.isi, deskripsi = bm.deskripsi, tanggal = bm.tanggal };
            List<bimbingan> temp = new List<bimbingan>();
            foreach (var i in result)
            {
                temp.Add(new bimbingan(){
                        id = i.id,
                        isi = i.isi,
                        tanggal = i.tanggal,
                        id_skripsi = i.id_skripsi,
                        deskripsi = i.deskripsi
                });
            }
            return View(new GridModel<bimbingan>() { Data = temp });
            
        }

        
        #endregion

        public ActionResult JadwalBimbingan()
        {
            return PartialView();
        }
       
        public ActionResult PengaturanKalender()
        {
            return PartialView();
        }

        

        //Pemesanan Jadwal
        [GridAction]
        public ActionResult _SelectJadwal()
        {
            return bindingJadwal(2);
        }
        [GridAction]
        public ActionResult _DeleteJadwal()
        {
            return bindingJadwal(3);
        }
        protected ViewResult bindingJadwal(int id)
        {

            List<DosenJadwalTerpesan> temp = new List<DosenJadwalTerpesan>();

            temp.Add(new DosenJadwalTerpesan()
            {
                id = 5,
                npm = "2010730001",
                tanggalPesan = "12/09/2010",
                waktu = "15:00",
                nama = "Jill",
                status = "disetujui",
                pesan ="Algoritma A"
            });
            temp.Add(new DosenJadwalTerpesan()
            {
                id =7,
                npm = "2010730088",
                tanggalPesan = "17/09/2010",
                waktu = "17:00",
                nama = "James",
                status = "dibatalkan",
                pesan = "Kontrak Kerja"
            });
            temp.Add(new DosenJadwalTerpesan()
            {
                id = 8,
                npm = "2010730099",
                tanggalPesan = "12/09/2010",
                waktu = "15:00",
                nama = "Jaime",
                status = "menunggu",
                pesan = "Implementasi"
            });
            return View(new GridModel<DosenJadwalTerpesan>
            {
                Data = temp
            });
        }
        public ActionResult FormBimbingan()
        {
            return PartialView();
        }
        

    }
}

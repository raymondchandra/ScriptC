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
            return PartialView();
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
        public ActionResult Skripsi1()
        {

            return PartialView();
        }
        public ActionResult Skripsi2()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult _SelectBimbinganMahasiswa1()
        {
            int id_skripsi = 1;

            string username = Session["username"].ToString();
            var sNIK = db.dosens.Where(x => x.username == username).Single<dosen>();
            string nNIK = sNIK.NIK;

            return bindingBimbinganMahasiswa(nNIK, id_skripsi);
        }
        [GridAction]
        public ActionResult _SelectBimbinganMahasiswa2()
        {
            int id_skripsi = 2;

            string username = Session["username"].ToString();
            var sNIK = db.dosens.Where(x => x.username == username).Single<dosen>();
            string nNIK = sNIK.NIK;
            return bindingBimbinganMahasiswa(nNIK, id_skripsi);
        }

        public ViewResult bindingBimbinganMahasiswa(string nik, int id_skripsi)
        {
            
            var result = from sk in db.skripsis
                         join mah in db.mahasiswas on sk.NPM_mahasiswa equals mah.NPM
                         join tp in db.topiks on sk.id_topik equals tp.id
                         where (sk.NIK_dosen_pembimbing.Equals( nik))
                         select new DosenMuridBimbinganContainer{ id = sk.id, judul = tp.judul, namaMahasiswa= mah.nama, npm = mah.NPM };
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
        [HttpPost]
        public int EditBimbingan(int id_bimbingan, string judul, string deskripsi)
        {
            ViewBag.id_bimbingan = id_bimbingan;
            bimbingan result = db.bimbingans.Where<bimbingan>(x => x.id == id_bimbingan).Single<bimbingan>();
            result.isi = judul;
            result.deskripsi = deskripsi;
            try
            {
                db.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                return 0;
            }
            
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

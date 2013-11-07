using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;

namespace Proyek_Informatika.Controllers.Mahasiswa
{
    
    public class BimbinganController : Controller
    {
        SkripsiAutoContainer db = new SkripsiAutoContainer();
        //
        // GET: /Bimbingan/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PemesananIndex()
        {
            return PartialView();
        }

        public ActionResult PemesananKalender()
        {
            return PartialView();
        }

        public ActionResult PemesananSummary()
        {
            return PartialView();
        }

        public ActionResult KartuBimbingan()
        {
            return PartialView();
        }

        public ActionResult DetilBimbingan()
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

            List<MahasiswaJadwalPemesanan> temp = new List<MahasiswaJadwalPemesanan>();

            temp.Add(new MahasiswaJadwalPemesanan()
            {
                id = 5,
                status = "disetujui",
                tanggalPesan = "12/09/2010",
                waktu = "15:00",
                namaDosen = "Ani"
            });
            temp.Add(new MahasiswaJadwalPemesanan()
            {
                id = 7,
                status = "dibatalkan",
                tanggalPesan = "12/09/2010",
                waktu = "17:00",
                namaDosen = "Berto"
            });
            temp.Add(new MahasiswaJadwalPemesanan()
            {
                id = 6,
                status = "menunggu",
                tanggalPesan = "17/09/2010",
                waktu = "19:00",
                namaDosen = "Cristy"
            });
            return View(new GridModel<MahasiswaJadwalPemesanan>
            {
                Data = temp
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult EditBimbingan(int id)
        {
            bimbingan result = db.bimbingans.Where<bimbingan>(x => x.id == id).Single<bimbingan>();
            TryUpdateModel<bimbingan>(result);
            int tipe = result.id_skripsi;
            db.SaveChanges();
            return bindingKartuBimbingan(tipe);
        }
        [GridAction]
        public ActionResult _SelectKartuBimbingan()
        {
            int id_skripsi = 1;
            //int id_skripsi = Int32.Parse(Session["id_skripsi"].ToString());
            return bindingKartuBimbingan(id_skripsi);
        }
        protected ViewResult bindingKartuBimbingan(int id_skripsi)
        {
            var result = (from bm in db.bimbingans
                          where bm.id_skripsi == id_skripsi
                          select new { id_skripsi = bm.id_skripsi, id = bm.id, isi = bm.isi, deskripsi = bm.deskripsi, tanggal = bm.tanggal });
            
            List<bimbingan> temp = new List<bimbingan>();

            foreach (var x in result)
            {
                temp.Add(new bimbingan() { id = x.id, id_skripsi = x.id_skripsi, deskripsi = x.deskripsi, isi = x.isi, tanggal = x.tanggal });
            }
            return View(new GridModel<bimbingan>{ Data = temp});
        }
    }
}

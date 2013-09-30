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

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult JadwalBimbingan()
        {
            return PartialView();
        }
        public ActionResult KartuBimbingan()
        {
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
        [GridAction]
        public ActionResult _SelectBimbinganMahasiswa()
        {
            return bindingBimbinganMahasiswa(0);
        }

        public ViewResult bindingBimbinganMahasiswa(int id)
        {
            List<DosenMuridBimbingan> temp;

            temp = new List<DosenMuridBimbingan>();

            temp.Add(new DosenMuridBimbingan() {
                id = 1,
                judul = "JST",
                jumlahBimbingan = 2,
                namaMahasiswa = "Hanzolo",
                npm = "2011730050"
            });

            temp.Add(new DosenMuridBimbingan()
            {
                id = 3,
                judul = "JST",
                jumlahBimbingan = 4,
                namaMahasiswa = "Batman",
                npm = "2011730080"
            });

            return View(new GridModel<DosenMuridBimbingan>() { Data = temp });

        }
        [GridAction]
        public ActionResult _SelectDosenKartu()
        {
            return bindingDosenKartu(0);
        }
        protected ViewResult bindingDosenKartu(int id)
        {
            List<DosenKartuBimbingan> temp;

            temp = new List<DosenKartuBimbingan>();

            temp.Add(new DosenKartuBimbingan()
            {
                id = 1,
                bahasan = "BAB 2",
                tanggal = "21/12/2013"
            });
            temp.Add(new DosenKartuBimbingan()
            {
                id = 1,
                bahasan = "BAB 3",
                tanggal = "31/12/2013"
                
            });

            return View(new GridModel<DosenKartuBimbingan> { Data = temp });
        }
    }
}

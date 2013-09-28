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
                namaDosen = "Lionov"
            });
            temp.Add(new MahasiswaJadwalPemesanan()
            {
                id = 7,
                status = "dibatalkan",
                tanggalPesan = "12/09/2010",
                waktu = "17:00",
                namaDosen = "Lionov"
            });
            temp.Add(new MahasiswaJadwalPemesanan()
            {
                id = 6,
                status = "menunggu",
                tanggalPesan = "17/09/2010",
                waktu = "19:00",
                namaDosen = "Lionov"
            });
            return View(new GridModel<MahasiswaJadwalPemesanan>
            {
                Data = temp
            });
        }

        //Kartu Bimbingan
        [GridAction]
        public ActionResult _SelectKartuBimbingan()
        {
            return bindingKartuBimbingan();
        }
        protected ViewResult bindingKartuBimbingan()
        {
            List<MahasiswaKartuBimbingan> temp;

            temp = new List<MahasiswaKartuBimbingan>();

            temp.Add(new MahasiswaKartuBimbingan()
            {
                id = 1,
                bahasan = "BAB 2",
                namaDosen = "Lionov",
                tanggal = "21/12/2013"
            });
            temp.Add(new MahasiswaKartuBimbingan()
            {
                id = 1,
                bahasan = "BAB 3",
                namaDosen = "Lionov",
                tanggal = "31/12/2013"
            });

            return View(new GridModel<MahasiswaKartuBimbingan> { Data = temp });
        }
    }
}

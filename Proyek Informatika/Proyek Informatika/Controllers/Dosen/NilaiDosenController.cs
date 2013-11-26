using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
namespace Proyek_Informatika.Controllers.Dosen
{

    public class NilaiDosenController : Controller
    {
        //
        // GET: /NilaiDosen/
        SkripsiAutoContainer db = new SkripsiAutoContainer();
        #region ui
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult FormNilai(int id_skripsi)
        {
            var result = db.skripsis.Where(x => x.id == id_skripsi).Single();

            ViewBag.id = id_skripsi;
            ViewBag.NPM = result.NPM_mahasiswa;
            ViewBag.nama = result.mahasiswa.nama;
            ViewBag.judul = result.topik.judul;

            var resultNilai = db.nilais.Where(x => x.id_skripsi == id_skripsi && x.kategori_nilai.tipe == "general").OrderBy(x => x.kategori_nilai.urutan);

            List<Tuple<string, int, int, double>> listNilai = new List<Tuple<string, int, int, double>>();
            int tempCount = resultNilai.Count();
            if (resultNilai.Count() == 0)
            {
                var kategori_nilai = db.kategori_nilai.Where(x => x.jenis_skripsi_id == 1).OrderBy(x => x.urutan);
                foreach (var item in kategori_nilai)
                {
                    if (tempCount != 1 || item.kategori != "presentasi")
                    {
                        db.nilais.Add(new nilai() { angka = 0, NIK_pengisi = getNik(), id_skripsi = id_skripsi, kategori = item.id, submitted = 0 });
                    }
                    if (item.tipe != "final")
                    {
                        listNilai.Add(new Tuple<string, int, int, double>(item.kategori, item.id, item.bobot, 0));
                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                { }

            }
            else
            {

                foreach (var item in resultNilai)
                {
                    listNilai.Add(new Tuple<string, int, int, double>(item.kategori_nilai.kategori, item.kategori, item.kategori_nilai.bobot, item.angka));
                }
            }
            ViewBag.status = getStatusNilaiBySkripsi(id_skripsi);
            ViewData["nilai"] = listNilai;
            return PartialView();
        }
        #endregion
        [HttpPost]
        [GridAction]
        public ActionResult SelectMahasiswa()
        {
            int skripsi = 1;

            string username = Session["username"].ToString();
            int periode = int.Parse(Session["id-semester"].ToString());

            var sNIK = db.dosens.Where(x => x.username == username).Single<dosen>();
            string nNIK = sNIK.NIK;

            return bindingMahasiswa(nNIK, periode, skripsi);
        }

        //binding list smua mahasiswa yang dibimbing
        public ViewResult bindingMahasiswa(string nik, int periode, int jenis_skripsi)
        {

            var result = from sk in db.skripsis
                         where (sk.NIK_dosen_pembimbing == nik && sk.jenis == jenis_skripsi && sk.id_semester_pengambilan == periode)
                         select new DosenMuridNilaiContainer { id = sk.id, judul = sk.topik.judul, namaMahasiswa = sk.mahasiswa.nama, npm = sk.mahasiswa.NPM };
            List<DosenMuridNilaiContainer> temp = result.ToList();

            foreach (var item in temp)
            {
                item.status = getStatusNilai(item.id);
            }

            //foreach (var i in result)
            //{
            //    temp.Add(new DosenMuridBimbinganContainer() { id = i.id, judul = i.judul, namaMahasiswa = i.nama, npm = i.NPM });
            //}
            return View(new GridModel<DosenMuridNilaiContainer>() { Data = temp });

        }

        public bool SubmitNilai(int id = 0, int kategori = 0, double nilai = 0, byte status = 0, double nilaiAkhir = 0)
        {
            try
            {
                nilai result = db.nilais.Where(x => x.id_skripsi == id && x.kategori == kategori).SingleOrDefault<nilai>();
                result.id_skripsi = id;
                result.kategori = kategori;
                result.angka = nilai;
                result.submitted = status;
            }
            catch
            {
                var result = db.nilais.Where<nilai>(x => x.id_skripsi == id && x.kategori == kategori);
                int count = 0;
                foreach (var item in result)
                {
                    if (count > 0)
                    {
                        db.nilais.Remove(item);
                    }
                    count++;
                }
            }

            nilai result2 = db.nilais.Where(x => x.id_skripsi == id && x.kategori_nilai.kategori == "nilaiAkhir").SingleOrDefault<nilai>();
            result2.angka = nilaiAkhir;
            result2.submitted = status;
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string getNik()
        {
            string username = Session["username"].ToString();
            string nik = db.dosens.Where(x => x.username == username).Single().NIK;
            return nik;
        }
        private int getStatusNilaiBySkripsi(int id)
        {
            int countNilai = db.nilais.Where(x => x.id_skripsi == id).Count();

            var result = db.nilais.Where(x => x.id_skripsi == id && x.submitted == 0 && x.kategori_nilai.tipe == "final");
            if (countNilai == 0)
            {
                return 0;
            }
            if (result.Count() > 0)
            {
                return 0;
            }
            return 1;
        }
        private string getStatusNilai(int id)
        {
            int countNilai = db.nilais.Where(x => x.id_skripsi == id).Count();

            var result = db.nilais.Where(x => x.id_skripsi == id && x.submitted == 0 && x.kategori_nilai.tipe == "final");
            if (countNilai == 0)
            {
                return "Belum Terisi";
            }
            if (result.Count() > 0)
            {
                return "Belum Terisi";
            }
            return "Sudah Terisi";
        }
    }
}

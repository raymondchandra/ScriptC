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

            ViewBag.id = result.id;
            ViewBag.NPM = result.NPM_mahasiswa;
            ViewBag.nama = result.mahasiswa.nama;
            ViewBag.judul = result.topik.judul;

            var resultNilai = db.nilais.Where(x => x.id_skripsi == id_skripsi).OrderBy(x=>x.kategori_nilai.urutan);

            List<Tuple<string,int, double>> listNilai = new List<Tuple<string,int,double>>();
            foreach (var item in resultNilai)
            {
                listNilai.Add(new Tuple<string,int, double>(item.kategori_nilai.kategori, item.kategori_nilai.bobot, item.angka));
            }
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
                         select new DosenMuridBimbinganContainer { id = sk.id, judul = sk.topik.judul, namaMahasiswa = sk.mahasiswa.nama, npm = sk.mahasiswa.NPM };
            List<DosenMuridBimbinganContainer> temp = result.ToList<DosenMuridBimbinganContainer>();

            //foreach (var i in result)
            //{
            //    temp.Add(new DosenMuridBimbinganContainer() { id = i.id, judul = i.judul, namaMahasiswa = i.nama, npm = i.NPM });
            //}
            return View(new GridModel<DosenMuridBimbinganContainer>() { Data = temp });

        }

    }
}

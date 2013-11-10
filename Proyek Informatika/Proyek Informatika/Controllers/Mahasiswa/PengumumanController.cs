using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;

namespace Proyek_Informatika.Controllers.Mahasiswa
{
    public class PengumumanController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        private HomeController hc = new HomeController();
        //
        // GET: /Pengumuman/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pengumuman()
        {
            return PartialView();
        }

        #region pengumuman

        [AcceptVerbs(HttpVerbs.Post)]
        public string SelectPengumuman()
        {

            string username = (string)Session["username"];
            int idSemester = int.Parse(Session["id-semester"].ToString());
            var npm = db.mahasiswas.Where(m => m.username == username).First().NPM;
            var skripsi = db.skripsis.Where(s => (s.NPM_mahasiswa == npm && s.id_semester_pengambilan == idSemester)).First();
            byte jenis = skripsi.jenis;
            var dosen = db.dosens.Where(d => d.NIK == skripsi.NIK_dosen_pembimbing).First().username;


            List<PengumumanContainer> listResult = (from p in db.pengumumen
                                                    where (p.pembuat == "Admin" || p.pembuat == dosen)
                                                    orderby p.tanggal descending
                                                    select new PengumumanContainer { id = p.id, target = p.target, judul = p.judul, isi = p.isi, pembuat = p.pembuat, tanggal = p.tanggal }).ToList();

            int[] skripsi1 = { 2, 3, 6, 7, 10, 11, 14, 15 };
            int[] skripsi2 = { 4, 5, 6, 7, 12, 13, 14, 15 };
            int[] array;



            if (jenis == 1)
            {
                array = skripsi1;
            }
            else
            {
                array = skripsi2;
            }

            string result = "";
            foreach (PengumumanContainer pc in listResult)
            {
                if (Array.IndexOf(array, pc.target) != -1)
                {
                    result += hc.format(pc);
                }
            }
            return result;
        }
        #endregion
    }
}

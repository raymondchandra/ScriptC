using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

namespace Proyek_Informatika.Controllers.Mahasiswa
{
    public class ProfileController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();

        #region view
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Biodata()
        {
            return PartialView();
        }

        public ActionResult Sejarah()
        {
            return PartialView();
        }


        public ActionResult Pengaturan()
        {
            return PartialView();
        }
        #endregion

        #region biodata
        [HttpPost]
        public JsonResult GetBiodata()
        {
            string username = (string)Session["username"];
            mahasiswa m = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.username == username).First();
            return Json(new { m.NPM, m.nama, m.email, m.nomor_telepon });
        }

        [HttpPost]
        public string SaveNama(mahasiswa model)
        {
            if (model.nama == null || model.nama == "")
            {
                return "Field nama harus diisi!";
            }
            string username = (string)Session["username"];
            mahasiswa m = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.username == username).First();
            m.nama = model.nama;
            if (TryUpdateModel(m))
            {
                db.SaveChanges();
            }
            return "Nama berhasil disimpan.";
        }

        [HttpPost]
        public string SaveEmail(mahasiswa model)
        {
            if (model.email == null || model.email == "")
            {
                return "Field email harus diisi!";
            }
            string username = (string)Session["username"];
            mahasiswa m = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.username == username).First();
            m.email = model.email;
            if (TryUpdateModel(m))
            {
                db.SaveChanges();
            }
            return "Email berhasil disimpan.";
        }

        [HttpPost]
        public string SaveTelepon(mahasiswa model)
        {
            if (model.nomor_telepon == null || model.nomor_telepon == "")
            {
                return "Field telepon harus diisi!";
            }
            string username = (string)Session["username"];
            mahasiswa m = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.username == username).First();
            m.nomor_telepon = model.nomor_telepon;
            if (TryUpdateModel(m))
            {
                db.SaveChanges();
            }
            return "Telepon berhasil disimpan.";
        }

        #endregion

        #region sejarah
        protected ViewResult bindingTable(int id)
        {
            int idSemester = int.Parse(Session["id-semester"].ToString());
            var username = (string)Session["username"];
            List<SejarahMahasiswaContainer> listResult = (from m in db.mahasiswas
                                                          join s in db.skripsis on m.NPM equals s.NPM_mahasiswa
                                                          join t in db.topiks on s.id_topik equals t.id
                                                          join d in db.dosens on t.NIK_pembimbing equals d.NIK
                                                          join j in db.jenis_skripsi on s.jenis equals j.id
                                                          join x in db.semesters on s.id_semester_pengambilan equals x.id
                                                          where m.username == username
                                                          where s.id_semester_pengambilan < idSemester //yang sudah lewat
                                                          select new SejarahMahasiswaContainer
                                                          {
                                                              periode = x.periode_semester,
                                                              jenis = j.nama_jenis,
                                                              topik = t.judul,
                                                              pembimbing = d.nama,
                                                              nilai = s.nilai_akhir
                                                          }).ToList();

            return View(new GridModel<SejarahMahasiswaContainer>
            {
                Data = listResult
            });
        }

        [GridAction]
        public ActionResult _SelectSejarah()
        {
            return bindingTable(0);
        }
        #endregion


    }
}
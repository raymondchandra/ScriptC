using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class ProfileDosenController : Controller
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
            dosen d = db.dosens.Where(dosenTemp => dosenTemp.username == username).First();
            return Json(new { d.NIK, d.nama, d.email });
        }

        [HttpPost]
        public string SaveNama(dosen model)
        {
            if (model.nama == null || model.nama == "")
            {
                return "Field nama harus diisi!";
            }
            string username = (string)Session["username"];
            dosen d = db.dosens.Where(dosenTemp => dosenTemp.username == username).First();
            d.nama = model.nama;
            if (TryUpdateModel(d))
            {
                db.SaveChanges();
            }
            return "Nama berhasil disimpan.";
        }

        [HttpPost]
        public string SaveEmail(dosen model)
        {
            if (model.email == null || model.email == "")
            {
                return "Field email harus diisi!";
            }
            string username = (string)Session["username"];
            dosen d = db.dosens.Where(dosenTemp => dosenTemp.username == username).First();
            d.email = model.email;
            if (TryUpdateModel(d))
            {
                //db.topiks.Add(topik);
                db.SaveChanges();
            }
            return "Email berhasil disimpan.";
        }
        #endregion

        #region sejarah
        protected ViewResult bindingTable(int id)
        {
            int idSemester = int.Parse(Session["id-semester"].ToString());
            var username = (string)Session["username"];
            List<SejarahDosenContainer> listResult = (from m in db.mahasiswas
                                                          join s in db.skripsis on m.NPM equals s.NPM_mahasiswa
                                                          join t in db.topiks on s.id_topik equals t.id
                                                          join d in db.dosens on t.NIK_pembimbing equals d.NIK
                                                          join j in db.jenis_skripsi on s.jenis equals j.id
                                                          join x in db.semesters on s.id_semester_pengambilan equals x.id
                                                          where d.username == username
                                                          where s.id_semester_pengambilan < idSemester //yang sudah lewat
                                                          select new SejarahDosenContainer
                                                          {
                                                              periode = x.periode_semester,
                                                              jenis = j.nama_jenis,
                                                              topik = t.judul,
                                                              npm = m.NPM,
                                                              mahasiswa = m.nama,
                                                              nilai = s.nilai_akhir
                                                          }).ToList();

            return View(new GridModel<SejarahDosenContainer>
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
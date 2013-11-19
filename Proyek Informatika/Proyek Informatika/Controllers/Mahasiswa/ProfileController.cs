using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using relmon.Controllers.Utilities;
using Telerik.Web.Mvc;


namespace Proyek_Informatika.Controllers.Mahasiswa
{
    public class ProfileController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        private UploadController uc = new UploadController();

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
        public JsonResult GetData()
        {
            int idSemester = int.Parse(Session["id-semester"].ToString());
            string username = (string)Session["username"];
            mahasiswa m = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.username == username).First();
            skripsi s = db.skripsis.Where(skripsiTemp => (skripsiTemp.NPM_mahasiswa == m.NPM && skripsiTemp.id_semester_pengambilan == idSemester)).First();
            dosen d = db.dosens.Where(dosenTemp => dosenTemp.NIK == s.NIK_dosen_pembimbing).First();
            topik t = db.topiks.Where(topikTemp => topikTemp.id == s.id_topik).First();
            string foto = uc.GetPathFoto(d.foto);
            return Json(new { foto, d.NIK, d.nama, d.email, s.jenis, t.judul });
        }


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

            this.setAktif();
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

            this.setAktif();
            return "Telepon berhasil disimpan.";
        }

        #endregion

        public void setAktif()
        {
            string username = (string)Session["username"];
            mahasiswa m = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.username == username).First();
            if (m.status == "nonaktif")
            {
                if (m.nama != null && m.nama != "")
                {
                    if (m.nomor_telepon != null && m.nomor_telepon != "")
                    {
                        m.status = "aktif";
                        if (TryUpdateModel(m))
                        {
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

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
                                                          where s.id_semester_pengambilan <= idSemester //yang sudah lewat + yg skrg
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

        #region foto
        public String GetFoto()
        {
            string username = (string)Session["username"];
            mahasiswa m = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.username == username).First();
            return uc.GetPathFoto(m.foto);
        }

        public ActionResult SaveFoto(IEnumerable<HttpPostedFileBase> attachmentsFoto)
        {
            string username = (string)Session["username"];
            // The Name of the Upload component is "attachments" 
            foreach (var file in attachmentsFoto)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                var extension = Path.GetExtension(file.FileName);
                var physicalPath = Path.Combine(Server.MapPath("~/Content"), "photo", username + extension);
                var folderPath = Path.Combine(Server.MapPath("~/Content"), "photo");
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);

                //save ke database
                mahasiswa m = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.username == username).First();
                m.foto = physicalPath;
                if (TryUpdateModel(m))
                {
                    db.SaveChanges();
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public string RemoveFoto()
        {
            string username = (string)Session["username"];
            // The Name of the Upload component is "attachments" 

            //select dari database
            mahasiswa m = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.username == username).First();

            if (System.IO.File.Exists(m.foto))
            {
                // The files are not actually removed in this demo
                System.IO.File.Delete(m.foto);
            }
            m.foto = "";
            if (TryUpdateModel(m))
            {
                db.SaveChanges();
            }

            return "";
        }
        #endregion


    }
}
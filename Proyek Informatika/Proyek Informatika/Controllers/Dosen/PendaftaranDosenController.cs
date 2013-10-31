using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class PendaftaranDosenController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        //
        // GET: /Pengumpulan/
        #region view

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrasiMahasiswa()
        {
            return PartialView();
        }

        public ActionResult MahasiswaUpdate()
        {
            return PartialView();
        }

        #endregion

        #region grid mahasiswa

        [HttpPost]
        public JsonResult GetTopik()
        {
            int idSemester = int.Parse(Session["id-semester"].ToString());
            string username = (string)Session["username"];

            var listResult = (from t in db.topiks
                              join d in db.dosens on t.NIK_pembimbing equals d.NIK
                              where t.id_semester == idSemester
                              where t.keterangan == "tersedia"
                              where d.username == username
                              select new { t.id, t.judul }).ToList();

            return Json(listResult);
        }
        public JsonResult GetMahasiswa()
        {
            int idSemester = int.Parse(Session["id-semester"].ToString());
            string username = (string)Session["username"];

            var listResult = (from m in db.mahasiswas
                              join s in db.skripsis on m.NPM equals s.NPM_mahasiswa
                              join d in db.dosens on s.NIK_dosen_pembimbing equals d.NIK
                              where d.username == username
                              where (idSemester - s.id_semester_pengambilan) <= 2 //maks cuti hanya 1 semester
                              select new { m.NPM, m.nama }).ToList();

            return Json(listResult);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public string _InsertMahasiswa1(MahasiswaContainer model)
        {
            if(model.NPM ==null){
                return "Registrasi mahasiswa gagal! \nField NPM harus diisi!";
            }
            skripsi s = new skripsi();
            s.jenis = 1;
            s.pengambilan_ke = 1;

            string username = (string)Session["username"];
            var d = db.dosens.Where(dosenTemp => dosenTemp.username == username).SingleOrDefault();
            s.NIK_dosen_pembimbing = d.NIK;
            s.id_semester_pengambilan = int.Parse(Session["id-semester"].ToString());

            //topik
            var topik = db.topiks.Where(topikTemp => topikTemp.id == model.idTopik).SingleOrDefault();
            topik.keterangan = "tidak tersedia";
            s.id_topik = topik.id;
            if (TryUpdateModel(topik))
            {
                //db.topiks.Add(topik);
                db.SaveChanges();
            }

            //mahasiswa
            string result = "";
            var mahasiswa = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.NPM == model.NPM);
            if (mahasiswa.Count() == 0)
            {
                mahasiswa m = new mahasiswa();
                m.nama = " ";
                m.email = model.NPM + "@student.unpar.ac.id";

                var akun = db.akuns.Where(akunTemp => akunTemp.username == model.NPM).SingleOrDefault();
                if (akun == null)
                {
                    akun a = new akun();
                    a.username = model.NPM;
                    result += "\nAkun baru untuk mahasiswa berhasil dibuat. \nusername : " + model.NPM + "\npassword : Password";
                    a.aktif = 1;
                    a.password = EncodePassword("Password");
                    a.peran = 3;
                    if (TryUpdateModel(a))
                    {
                        db.akuns.Add(a);
                        db.SaveChanges();
                    }
                    m.username = a.username;
                }
                akun = db.akuns.Where(akunTemp => akunTemp.username == model.NPM).SingleOrDefault();

                m.status = "aktif";

                if (TryUpdateModel(m))
                {
                    db.mahasiswas.Add(m);
                    db.SaveChanges();
                }
                mahasiswa = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.NPM == model.NPM);
            }
            s.NPM_mahasiswa = mahasiswa.SingleOrDefault().NPM;

            if (TryUpdateModel(s))
            {
                db.skripsis.Add(s);
                db.SaveChanges();
            }
            return "Registrasi berhasil! " + result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public string _UpdateMahasiswa(MahasiswaContainer model)
        {
            skripsi s = db.skripsis.Where(skripsiTemp => skripsiTemp.id == model.idSkripsi).SingleOrDefault();

            //topik
            var topik = db.topiks.Where(topikTemp => topikTemp.id == s.id_topik).SingleOrDefault();
            topik.judul = model.judul;

            if (TryUpdateModel(topik))
            {
                //db.topiks.Add(topik);
                db.SaveChanges();
            }

            //mahasiswa
            string result = "";
            var mahasiswa = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.NPM == model.NPM);
            if (mahasiswa.Count() == 0)
            {
                mahasiswa m = new mahasiswa();
                m.nama = " ";
                m.email = model.NPM + "@student.unpar.ac.id";

                var akun = db.akuns.Where(akunTemp => akunTemp.username == model.NPM).SingleOrDefault();
                if (akun == null)
                {
                    akun a = new akun();
                    a.username = model.NPM;
                    result += "\nAkun baru untuk mahasiswa berhasil dibuat. \nusername : " + model.NPM + "\npassword : Password";
                    a.aktif = 1;
                    a.password = EncodePassword("Password");
                    a.peran = 3;
                    if (TryUpdateModel(a))
                    {
                        db.akuns.Add(a);
                        db.SaveChanges();
                    }
                    m.username = a.username;
                }
                akun = db.akuns.Where(akunTemp => akunTemp.username == model.NPM).SingleOrDefault();

                m.status = "aktif";

                if (TryUpdateModel(m))
                {
                    db.mahasiswas.Add(m);
                    db.SaveChanges();
                }
                mahasiswa = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.NPM == model.NPM);
            }
            s.NPM_mahasiswa = mahasiswa.SingleOrDefault().NPM;
            
            if (TryUpdateModel(s))
            {
                //db.skripsis.Add(s);
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }
            return "Edit berhasil!";
        }

        [GridAction]
        public ActionResult _SelectMahasiswa()
        {
            return bindingTable();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteMahasiswa(int id)
        {
            var s = db.skripsis.Where(skripsiTemp => skripsiTemp.id == id).SingleOrDefault();
            if (s != null)
            {
                db.skripsis.Remove(s);
                db.SaveChanges();
            }
            return bindingTable();
        }

        protected ViewResult bindingTable()
        {
            int idSemester = int.Parse(Session["id-semester"].ToString());
            var login = (string)Session["role"];
            var username = (string)Session["username"];
            List<MahasiswaContainer> listResult = (from m in db.mahasiswas
                                                   join s in db.skripsis on m.NPM equals s.NPM_mahasiswa
                                                   join t in db.topiks on s.id_topik equals t.id
                                                   join d in db.dosens on t.NIK_pembimbing equals d.NIK
                                                   join x in db.semesters on s.id_semester_pengambilan equals x.id
                                                   where d.username == username
                                                   where s.id_semester_pengambilan == idSemester
                                                   select new MahasiswaContainer
                                                   {
                                                       NPM = m.NPM,
                                                       namaMahasiswa = m.nama,
                                                       emailMahasiswa = m.email,
                                                       teleponMahasiswa = m.nomor_telepon,
                                                       status = m.status,
                                                       idSkripsi = s.id,
                                                       //idTopik = t.id,                                                  
                                                       judul = t.judul,
                                                       NIK = d.NIK,
                                                       //namaDosen = d.nama,
                                                       //emailDosen = d.email,
                                                       //idSemester = t.id_semester,
                                                       semester = x.periode_semester
                                                   }).ToList();

            return View(new GridModel<MahasiswaContainer>
            {
                Data = listResult
            });
        }


        #endregion
        private string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }
}

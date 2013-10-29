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

        public ActionResult MahasiswaInsert()
        {
            return PartialView();
        }

        public ActionResult MahasiswaUpdate()
        {
            return PartialView();
        }

        #endregion

        #region grid mahasiswa

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult MahasiswaInsert(MahasiswaView model)
        {
            skripsi s = new skripsi();
            s.jenis = 1;
            s.pengambilan_ke = 1;

            string username = (string)Session["username"];
            var d = db.dosens.Where(dosenTemp => dosenTemp.username == username).SingleOrDefault();
            s.NIK_dosen_pembimbing = d.NIK;

            s.id_semester_pengambilan = int.Parse(Session["id-semester"].ToString());

            //topik
            var topik = db.topiks.Where(topikTemp => topikTemp.judul == model.judul).SingleOrDefault();
            if (topik == null)
            {
                topik t = new topik();
                t.NIK_pembimbing = d.NIK;
                t.judul = model.judul;
                t.keterangan = "tidak tersedia";
                t.id_semester = s.id_semester_pengambilan;
                if (TryUpdateModel(t))
                {
                    db.topiks.Add(t);
                    db.SaveChanges();
                }
                topik = db.topiks.Where(topikTemp => topikTemp.judul == model.judul).SingleOrDefault();
            }
            topik.keterangan = "tidak tersedia";
            s.id_topik = topik.id;

            //mahasiswa
            var mahasiswa = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.NPM == model.NPM).SingleOrDefault();
            if (mahasiswa == null)
            {
                mahasiswa m = new mahasiswa();
                m.nama = model.NPM;
                m.email = model.NPM + "@student.unpar.ac.id";
                //m.nomor_telepon = model.NPM;
                //m.foto = "img";

                var akun = db.akuns.Where(akunTemp => akunTemp.username == model.NPM).SingleOrDefault();
                if (akun == null)
                {
                    akun a = new akun();
                    a.username = model.NPM;
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
                mahasiswa = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.NPM == model.NPM).SingleOrDefault();
            }
            s.NPM_mahasiswa = mahasiswa.NPM;

            if (TryUpdateModel(s))
            {
                db.skripsis.Add(s);
                db.SaveChanges();
            }
            Session["redirect"] = "mahasiswa";
            Session["message"] = "insert";
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult MahasiswaUpdate(MahasiswaView model)
        {
            skripsi s = db.skripsis.Where(skripsiTemp => skripsiTemp.id == model.idSkripsi).SingleOrDefault();

            //topik
            var topik = db.topiks.Where(topikTemp => topikTemp.judul == model.judul).SingleOrDefault();
            if (topik == null)
            {
                topik t = new topik();
                t.NIK_pembimbing = s.NIK_dosen_pembimbing;
                t.judul = model.judul;
                t.keterangan = "tidak tersedia";
                t.id_semester = s.id_semester_pengambilan;
                if (TryUpdateModel(t))
                {
                    db.topiks.Add(t);
                    db.SaveChanges();
                }
                topik = db.topiks.Where(topikTemp => topikTemp.judul == model.judul).SingleOrDefault();
            }
            topik.keterangan = "tidak tersedia";
            s.id_topik = topik.id;

            //mahasiswa
            var mahasiswa = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.NPM == model.NPM).SingleOrDefault();
            if (mahasiswa == null)
            {
                mahasiswa m = new mahasiswa();
                m.nama = model.NPM;
                m.email = model.NPM + "@student.unpar.ac.id";
                //m.nomor_telepon = model.NPM;
                //m.foto = "img";

                var akun = db.akuns.Where(akunTemp => akunTemp.username == model.NPM).SingleOrDefault();
                if (akun == null)
                {
                    akun a = new akun();
                    a.username = model.NPM;
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
                mahasiswa = db.mahasiswas.Where(mahasiswaTemp => mahasiswaTemp.NPM == model.NPM).SingleOrDefault();
            }
            s.NPM_mahasiswa = mahasiswa.NPM;

            if (TryUpdateModel(s))
            {
                db.skripsis.Add(s);
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }
            Session["redirect"] = "mahasiswa";
            Session["message"] = "update";
            return RedirectToAction("Index", "Home");
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
            List<MahasiswaView> listResult = (from m in db.mahasiswas
                                              join s in db.skripsis on m.NPM equals s.NPM_mahasiswa
                                              join t in db.topiks on s.id_topik equals t.id
                                              join d in db.dosens on t.NIK_pembimbing equals d.NIK
                                              join x in db.semesters on s.id_semester_pengambilan equals x.id
                                              where d.username == username
                                              where s.id_semester_pengambilan == idSemester
                                              select new MahasiswaView
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

            return View(new GridModel<MahasiswaView>
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

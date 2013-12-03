using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
using System.Data;
using relmon.Controllers.Utilities;
namespace Proyek_Informatika.Controllers
{
    public class UserController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        private AccountController ac = new AccountController();
        private UploadController uc = new UploadController();
        //
        // GET: /User/

        #region Mahasiswa

        public ActionResult BiodataMahasiswa()
        {
            return PartialView();
        }

        public ActionResult SejarahMahasiswa()
        {
            return PartialView();
        }

        #endregion

        #region Dosen
        public ActionResult BiodataDosen()
        {
            return PartialView();
        }

        public ActionResult SejarahDosen()
        {
            return PartialView();
        }

        public ActionResult PengaturanDosen()
        {
            return PartialView();
        }
        #endregion

        public ActionResult Pengaturan()
        {
            return PartialView();
        }

        public ActionResult ListDosen()
        {
            return PartialView();
        }

        public ActionResult ListMahasiswa()
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

        public ActionResult Registrasi()
        {
            return PartialView();
        }










        

       
 #region DaftarMahasiswa

        public ActionResult DaftarMahasiswa()
        {
            //setting dropdown list
            var temp = (from s in db.semesters select s).ToList();
            List<semester> list_semester = new List<semester>();
            foreach (var t in temp)
            {
                list_semester.Add(new semester { id = t.id, periode_semester = t.periode_semester, isCurrent = t.isCurrent });               
            }
            ViewData["list_semester"] = list_semester;
            ViewData["id_selected_semester"] = db.semesters.Where(s => s.isCurrent == 1).FirstOrDefault().id;            

            return PartialView();
        }

        public ActionResult GridMahasiswa(int id_selected_semester)
        {
            if (id_selected_semester == -1)
            {
                ViewBag.id_selected_semester = db.semesters.Where(s => s.isCurrent == 1).FirstOrDefault().id;
            }
            else
            {
                ViewBag.id_selected_semester = id_selected_semester;
            }            
            return PartialView();
        }

        public ActionResult LihatDetail(string npm)
        {
            var mhs = db.mahasiswas.FirstOrDefault(m => m.NPM == npm);
            var skripsiList = db.skripsis.Where(s => s.NPM_mahasiswa == npm).ToList();
            if (skripsiList.Count != 0)
            {
                var max = db.skripsis.Where(s => s.NPM_mahasiswa == npm).Max(x => x.id);
                var skripsi = db.skripsis.SingleOrDefault(s => s.id == max);
                var topik = db.topiks.SingleOrDefault(t => t.id == skripsi.id_topik).judul;
                var nama_dosen = db.dosens.SingleOrDefault(d => d.NIK == skripsi.NIK_dosen_pembimbing).nama;
                var periode = db.semesters.SingleOrDefault(s => s.id == skripsi.id_semester_pengambilan).periode_semester;

                return Json(new
                {
                    success = true,
                    foto = uc.GetPathFoto(mhs.foto),
                    npm = npm,
                    nama = mhs.nama,
                    status = mhs.status,
                    jenis = (skripsi.jenis == 1 ? "Skripsi 1" : (skripsi.jenis == 2 ? "Skripsi 2" : "Lulus")),
                    pengambilan = skripsi.pengambilan_ke,
                    periode_pengambilan = periode,
                    topik = topik,
                    pembimbing = nama_dosen,
                    nilai = skripsi.nilai_akhir
                });
            }
            else
            {
                return Json(new {
                    success = false
                });
            }
            
        }

        public bool Aktivasi_Melanjutkan(string npm)
        {
            var mhs = db.mahasiswas.SingleOrDefault(m => m.NPM == npm);
            var max_skripsi_idx = db.skripsis.Where(s => s.NPM_mahasiswa == npm).Max(s => s.id);
            var skripsi = db.skripsis.SingleOrDefault(s => s.id ==  max_skripsi_idx);
            
            //check validity
            if (skripsi.nilai_akhir == null || skripsi.nilai_akhir == "E") return false;

            //create new skripsi entry
            skripsi lanjutan = new skripsi();
            if (skripsi.jenis == 1)
            {
                lanjutan.jenis = 2;
                //activate mahasiswa
                mhs.status = "aktif";
            }
            else if (skripsi.jenis == 2) lanjutan.jenis = 3;
            lanjutan.pengambilan_ke = 1;
            lanjutan.NIK_dosen_pembimbing = skripsi.NIK_dosen_pembimbing;
            lanjutan.NPM_mahasiswa = npm;
            lanjutan.id_semester_pengambilan = db.semesters.SingleOrDefault(sem => sem.isCurrent == 1).id;
            lanjutan.id_topik = skripsi.id_topik;
            
            db.skripsis.Add(lanjutan);

            
            try
            {
                db.Entry(mhs).State = System.Data.EntityState.Modified;
                db.SaveChanges();                
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Aktivasi_Mengulang(string npm)
        {
            var mhs = db.mahasiswas.SingleOrDefault(m => m.NPM == npm);
            var max_skripsi_idx = db.skripsis.Where(s => s.NPM_mahasiswa == npm).Max(s => s.id);
            var skripsi = db.skripsis.SingleOrDefault(s => s.id == max_skripsi_idx);

            //check validity
            if (skripsi.nilai_akhir == null || skripsi.pengambilan_ke == 2 || skripsi.nilai_akhir == "A" || skripsi.nilai_akhir == "B" || skripsi.nilai_akhir == "C") 
                return false;

            //create new skripsi entry
            skripsi ulangan = new skripsi();
            ulangan.jenis = skripsi.jenis;
            ulangan.pengambilan_ke = 2;
            ulangan.NIK_dosen_pembimbing = skripsi.NIK_dosen_pembimbing;
            ulangan.NPM_mahasiswa = npm;
            ulangan.id_semester_pengambilan = db.semesters.SingleOrDefault(sem => sem.isCurrent == 1).id;
            ulangan.id_topik = skripsi.id_topik;

            db.skripsis.Add(ulangan);

            //activate mahasiswa
            mhs.status = "aktif";
            try
            {
                db.Entry(mhs).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Aktivasi_Mengganti_Topik(string npm, int id_topik)
        {
            var mhs = db.mahasiswas.SingleOrDefault(m => m.NPM == npm);
            var topik = db.topiks.SingleOrDefault(t => t.id == id_topik);

            //check validity
            //<no validity checking, always can ganti topik>

            //create new skripsi entry
            skripsi baru = new skripsi();
            baru.jenis = 1;
            baru.pengambilan_ke = 1;
            baru.NIK_dosen_pembimbing = topik.NIK_pembimbing;
            baru.NPM_mahasiswa = npm;
            baru.id_semester_pengambilan = db.semesters.SingleOrDefault(s => s.isCurrent == 1).id;
            baru.id_topik = id_topik;

            db.skripsis.Add(baru);

            //topik is now unavailable
            topik.keterangan = "tidak tersedia";

            //activate mahasiswa
            mhs.status = "aktif";
            
            try
            {
                db.Entry(topik).State = System.Data.EntityState.Modified;
                db.Entry(mhs).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        
        protected ViewResult bindingMahasiswa(int id_selected_semester)
        {
            List<mahasiswa> list_mahasiswa = (from m in db.mahasiswas select m).ToList();
            List<AlbertContainer.MahasiswaForGrid> list_mahasiswa_grid = new List<AlbertContainer.MahasiswaForGrid>();
            foreach (mahasiswa mhs in list_mahasiswa)
            {
                var skripsiList = db.skripsis.Where(s => s.NPM_mahasiswa == mhs.NPM).ToList();
                if (skripsiList.Count != 0)
                {
                    int max = skripsiList.Max(s => s.id);
                    var skripsi = db.skripsis.SingleOrDefault(sk => sk.id == max);
                    list_mahasiswa_grid.Add(new AlbertContainer.MahasiswaForGrid
                    {
                        NPM = mhs.NPM,
                        nama = mhs.nama,
                        jenis_skripsi = (skripsi.jenis == 1 ? "Skripsi 1" : (skripsi.jenis == 2 ? "Skripsi 2" : "Lulus")),
                        status = mhs.status,
                        dosen_pembimbing = db.dosens.Where(d => d.NIK == skripsi.NIK_dosen_pembimbing).SingleOrDefault().inisial,
                        periode_pengambilan = db.semesters.Where(x => x.id == skripsi.id_semester_pengambilan).Select(x => x.periode_semester).SingleOrDefault(),
                        pengambilan_ke = skripsi.pengambilan_ke
                    });
                }
                else
                {
                    list_mahasiswa_grid.Add(new AlbertContainer.MahasiswaForGrid
                    {
                        NPM = mhs.NPM,
                        nama = mhs.nama,
                        jenis_skripsi = "N/A",
                        status = mhs.status,
                        dosen_pembimbing = "N/A",
                        periode_pengambilan = "N/A",
                        pengambilan_ke = 0
                    });
                }
                
            }
                        
            return View(new GridModel<AlbertContainer.MahasiswaForGrid>
            {
                Data = list_mahasiswa_grid
            });
        }
        
        [GridAction]
        public ActionResult _SelectMahasiswaKoor(int semester = 0)
        {
            return bindingMahasiswa(semester);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveMahasiswa(int semester = 0)
        {

            return bindingMahasiswa(semester);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertMahasiswa()
        {
            return bindingMahasiswa(2);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteMahasiswa(int semester = 0)
        {

            return bindingMahasiswa(semester);
        }
        
        #endregion 

        
    #region Daftar Dosen
        public ActionResult DaftarDosen()
        {
            return PartialView();
        }

        protected ViewResult bindingDosen()
        {
            var tempList = db.dosens.ToList();

            List<DosenContainer> listDosen = new List<DosenContainer>();
            foreach (var item in tempList)
            {
                var aktif = (item.aktif == 1) ? "Aktif" : "Non Aktif";
                listDosen.Add(new DosenContainer()
                { 
                    email = item.email,
                    foto = (item.foto == null) ? "/Content/images/user.png" : Server.MapPath("~Content/photo") + "\\"+item.foto,
                    inisial = item.inisial,
                    nama = item.nama,
                    NIK = item.NIK,
                    username = item.username,
                    aktif = aktif
                });
            }
            return View(new GridModel<DosenContainer>
            {
                Data = listDosen
            });
        }

        [GridAction]
        public ActionResult _SelectDosen()
        {
            return bindingDosen();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveDosen(string NIK)
        {
            var getDosen = db.dosens.Where(p => p.NIK == NIK).SingleOrDefault();
            var usernameTemp = getDosen.username;
            TryUpdateModel(getDosen);

            if(getDosen.username != usernameTemp){
                //ganti akun
                var akun = db.akuns.Where(x => x.username == usernameTemp).SingleOrDefault();
                akun.aktif = 3;
                db.Entry(akun).State = EntityState.Modified;
                var checAkun = db.akuns.Where(x => x.username == getDosen.username).SingleOrDefault();
                if (checAkun != null)
                {

                }
                else
                {
                    akun newAkun = new akun();
                    newAkun.aktif = 1;
                    newAkun.password = ac.EncodePassword(getDosen.username);
                    newAkun.peran = 2;
                    newAkun.username = getDosen.username;
                    db.akuns.Add(newAkun);
                }
            }

            db.Entry(getDosen).State = EntityState.Modified;
            db.SaveChanges();

            return bindingDosen();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertDosen()
        {
            dosen dosen = new dosen();
            if (TryUpdateModel(dosen))
            {
                dosen.aktif = 1;
                var akun = db.akuns.Where(x => x.username == dosen.username).ToList();
                if (akun.Count == 0)
                {
                    //insert
                    akun newAkun = new akun();
                    newAkun.aktif = 1;
                    newAkun.password = ac.EncodePassword(dosen.username);
                    newAkun.peran = 2;
                    newAkun.username = dosen.username;
                    db.akuns.Add(newAkun);
                    db.dosens.Add(dosen);
                    db.SaveChanges();
                }
                else
                {
                    //error
                }
                
            }
            return bindingDosen();
        }

        public ActionResult Change_Status_Dosen(string NIK)
        {
            dosen a = db.dosens.SingleOrDefault(o => o.NIK == NIK);
            if (a.aktif == 1) a.aktif = 0;
            else a.aktif = 1;

            if (TryUpdateModel(a))
            {
                db.SaveChanges();
                return Json(new { success = true });
            }
            else return Json(new { success = false });
        }
    #endregion




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
using System.Data;

namespace Proyek_Informatika.Controllers.Koordinator
{
    public class SidangKoordinatorController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        //
        // GET: /Sidang/
#region Pengaturan Jadwal
        public ActionResult Jadwal_Navigator()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult _SelectJadwalSidang()
        {
            var listResult = (from table in db.ruangs
                              select table).ToList();

            return View(new GridModel<ruang>
            {
                Data = listResult
            });
        }


        

    #region Pengaturan
        public ActionResult Jadwal_Pengaturan(int semester_id)
        {
            ViewBag.semester_id = semester_id;
            return PartialView();
        }

        public bool SimpanPengaturanJadwal(periode_sidang periode_sidang)
        {
            var temp = db.periode_sidang.Where(x => x.semester_id == periode_sidang.semester_id).ToList();
            if (temp.Count == 0)
            {
                db.periode_sidang.Add(periode_sidang);
            }
            else
            {
                var update = temp.SingleOrDefault();
                update.semester_id = periode_sidang.semester_id;
                update.tipe_sidang = periode_sidang.tipe_sidang;
                update.start_date = periode_sidang.start_date;
                update.end_date = periode_sidang.end_date;
                db.Entry(update).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string GetStartDate(int semester_id, string tipe)
        {
            DateTime temp = db.periode_sidang.Where(x => x.semester_id == semester_id && x.tipe_sidang == tipe).Select(y => y.start_date).SingleOrDefault();
            if(temp.Year == 1){
                temp = DateTime.Now;
            }
            return temp.Day +"/"+ temp.Month +"/"+temp.Year;
        }

        public string GetEndDate(int semester_id, string tipe)
        {
            DateTime temp = db.periode_sidang.Where(x => x.semester_id == semester_id && x.tipe_sidang == tipe).Select(y => y.end_date).SingleOrDefault();
            if (temp.Year == 1)
            {
                temp = DateTime.Now;
            }
            return temp.Day + "/" + temp.Month + "/" + temp.Year;
        }
    #endregion

    #region Penempatan Mahasiswa
        public ActionResult Jadwal_Penempatan1()
        {
            return PartialView();
        }

        public ActionResult Jadwal_Penempatan2()
        {
            return PartialView();
        }
    #endregion

        [HttpPost]
        public JsonResult GetPeriodeSemester()
        {
            var listResultTemp = (from table in db.semesters
                                  select new
                                  {
                                      periode = table.periode_semester
                                  }).Distinct().ToList();
            return Json(listResultTemp);
        }

        [HttpPost]
        public int GetSemesterId(string periode)
        {
            return db.semesters.Where(x => x.periode_semester == periode).Select(y => y.id).SingleOrDefault();
        }
#endregion 

#region Lihat Jadwal
        public ActionResult Jadwal_Lihat()
        {
            return PartialView();
        }

        public ActionResult Jadwal_Lihat_Kalender(string periode,string tipe){
            var semId = this.GetSemesterId(periode);
            var result = (from table in db.periode_sidang
                          where (table.semester_id==semId && table.tipe_sidang == tipe)
                          select table).ToList();
            if (result.Count != 0)
            {
                periode_sidang getResult = result.SingleOrDefault();
                int[] start = { getResult.start_date.Day, getResult.start_date.Month, getResult.start_date.Year };
                int[] end = { getResult.end_date.Day, getResult.end_date.Month, getResult.end_date.Year };
                ViewBag.start_date = start;
                ViewBag.end_date = end;
                ViewBag.exist = true;
            }
            else
            {
                ViewBag.exist = false;
            }
            
            return PartialView();
        }
#endregion

        #region Pengaturan Sidang
        public ActionResult Sidang_Index()
        {
            ViewBag.username = Session["username"].ToString();
            return PartialView();
        }

        public ActionResult Sidang_Bobot1()
        {
            return PartialView();
        }

        public ActionResult Sidang_Bobot2()
        {
            return PartialView();
        }
        #endregion

        #region grid bobot
        [GridAction]
        public ActionResult _SelectBobot(string tipe, string jenisSkripsi)
        {
            return bindingTable(tipe);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveBobot(int id, string tipe)
        {
            var getKategori = db.kategori_nilai.Where(p => p.id == id).First();
            TryUpdateModel(getKategori);
            db.Entry(getKategori).State = EntityState.Modified;
            db.SaveChanges();
            return bindingTable(tipe);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertBobot(string tipe)
        {
            kategori_nilai kategori = new kategori_nilai();
            if (TryUpdateModel(kategori))
            {
                kategori.tipe = tipe;
                //kategori.jenis_skripsi = ;
                
                db.kategori_nilai.Add(kategori);
                db.SaveChanges();
            }
            return bindingTable(tipe);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteBobot(int id, string tipe)
        {
            var getKategori = db.kategori_nilai.Where(p => p.id == id).First();
            db.kategori_nilai.Remove(getKategori);
            db.SaveChanges();
            return bindingTable(tipe);
        }

        protected ViewResult bindingTable(string tipe)
        {
            var listResult = (from table in db.kategori_nilai
                              where (table.tipe == tipe && table.kategori != "general")
                              select table).ToList();

            return View(new GridModel<kategori_nilai>
            {
                Data = listResult
            });
        }
    #endregion

    #region ruang
        [GridAction]
        public ActionResult _SelectRuang()
        {
            return bindingTableRuang();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveRuang(int id)
        {
            var getRuang = db.ruangs.Where(p => p.id == id).First();
            TryUpdateModel(getRuang);
            db.Entry(getRuang).State = EntityState.Modified;
            db.SaveChanges();
            return bindingTableRuang();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertRuang()
        {
            ruang ruang = new ruang();
            if (TryUpdateModel(ruang))
            {
                db.ruangs.Add(ruang);
                db.SaveChanges();
            }
            return bindingTableRuang();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteRuang(int id)
        {
            var getRuang = db.ruangs.Where(p => p.id == id).First();
            db.ruangs.Remove(getRuang);
            db.SaveChanges();
            return bindingTableRuang();
        }

        protected ViewResult bindingTableRuang()
        {
            var listResult = (from table in db.ruangs
                              select table).ToList();

            return View(new GridModel<ruang>
            {
                Data = listResult
            });
        }
    #endregion

        public int GetBobot(string tipe)
        {
            var listResult = (from table in db.kategori_nilai
                              where (table.kategori == "general" && table.tipe == tipe)
                              select table).ToList();
            if (listResult.Count != 0)
            {
                var result = listResult.First();
                return result.bobot;
            }
            else
            {
                return 0;
            }
        }

        public int HitungPersentase(string pemberiNilai,int skripsi)
        {
            var listResult = (from table in db.kategori_nilai
                              where (table.tipe == pemberiNilai && table.kategori != "general")
                              select table).ToList();
            if (listResult.Count != 0)
            {
                
                return listResult.Sum(x=>x.bobot);
            }
            else
            {
                return 0;
            }
        }

        public bool SimpanBobotGeneral(kategori_nilai newRow)
        {
            var find = db.kategori_nilai.Where(x=>x.kategori=="general" && x.tipe == newRow.tipe).ToList();

            if (find.Count == 0)
            {
                db.kategori_nilai.Add(newRow);
            }
            else
            {
                var getKategori = find.First();
                getKategori.bobot = newRow.bobot;
                db.Entry(getKategori).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }



        
    }
}

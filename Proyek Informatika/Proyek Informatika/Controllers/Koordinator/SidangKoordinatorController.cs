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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PengaturanJadwal()
        {
            return PartialView();
        }

        public ActionResult PengaturanSidang()
        {
            return PartialView();
        }

        public ActionResult KomposisiNilai()
        {
            return PartialView();
        }

        public ActionResult KomposisiNilai2()
        {
            return PartialView();
        }

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

        public ActionResult Lihat()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult _SelectJadwalSidang()
        {
            var listResult = (from table1 in db.sidangs
                              join table2 in db.skripsis on table1.id_skripsi equals table2.id
                              join table3 in db.semesters on table2.id_semester_pengambilan equals table3.id
                              where (table3.periode_semester == "")
                              select new
                              {
                                  id = table1.id
                              }).ToList();

            return View(new GridModel<object>
            {
                Data = listResult
            });
        }

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
    }
}

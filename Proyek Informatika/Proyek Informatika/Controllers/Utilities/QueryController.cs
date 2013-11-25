using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;

namespace Proyek_Informatika.Controllers.Utilities
{
    public class QueryController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        
        public static bool IsAktif(string username){
            SkripsiAutoContainer db = new SkripsiAutoContainer();
            var check = db.mahasiswas.Where(x => x.username == username).ToList() ;
            if (check.Count != 1)
            {
                return false;
            }
            else
            {
                var getRow = check.SingleOrDefault();
                return (getRow.status == "aktif");
            }
        }
        [HttpPost]
        public JsonResult GetDosenList()
        {

            var listResultTemp = (from table in db.dosens
                                  where (table.aktif == 1)
                                  select new
                                  {
                                      NIK = table.NIK,
                                      nama = table.nama
                                  }).Distinct().ToList();

            return Json(listResultTemp);
        }

        [HttpPost]
        public JsonResult GetPeriodeSemester()
        {
            
            var listResultTemp = (from table in db.semesters
                                  select new
                                  {
                                      id = table.id,
                                      periode = table.periode_semester
                                  }).Distinct().ToList();
            
            return Json(listResultTemp);
        }

        
        public int GetSemesterId(string periode)
        {
            return db.semesters.Where(x => x.periode_semester == periode).Select(y => y.id).SingleOrDefault();
        }

        
        public string GetSemesterName(int id)
        {
            return db.semesters.Where(x => x.id == id).Select(y => y.periode_semester).SingleOrDefault();
        }

        public int GetThisIdFromNPM(string npm)
        {
            int semester = db.semesters.Where(semesterTemp => semesterTemp.isCurrent == 1).Select(y=>y.id).SingleOrDefault();
            return db.skripsis.Where(x => x.NPM_mahasiswa == npm && x.id_semester_pengambilan == semester).Select(y => y.id).SingleOrDefault();
        }

        public int GetCurrentSemester(){
            int getCurrentFromDb = db.semesters.Where(x => x.isCurrent == 1).Select(y=>y.id).SingleOrDefault();
            return getCurrentFromDb;
        }

		public JsonResult GetTopikList()
        {
            var list_topik = (from t in db.topiks 
                              where t.keterangan.Equals("tersedia")
                              select new
                              {
                                  id = t.id,
                                  judul = t.judul
                              }).Distinct().ToList();

            return Json(list_topik);
        }

        public string GetNamaPembimbing(int id_topik)
        {
            var nik = db.topiks.SingleOrDefault(t => t.id == id_topik).NIK_pembimbing;
            var nama = db.dosens.SingleOrDefault(d => d.NIK == nik).nama;
            return nama;
        }
    }
}

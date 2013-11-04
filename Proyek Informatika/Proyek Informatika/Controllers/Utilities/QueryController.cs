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
        
        [HttpPost]
        public JsonResult GetDosenList()
        {

            var listResultTemp = (from table in db.dosens
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

    }
}

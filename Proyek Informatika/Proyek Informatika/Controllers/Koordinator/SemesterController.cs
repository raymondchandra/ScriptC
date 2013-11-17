using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using System.Data;
using System.Globalization;

namespace Proyek_Informatika.Controllers.Koordinator
{
    public class SemesterController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();

        // GET: /Semester/

        public ActionResult JadwalSemester()
        {
            int id_curr_semester = db.semesters.Where(x => x.isCurrent == 1).ToList().First().id;
            ViewData["schedule_skripsi1"] = db.jadwal_semester.Where(x => x.id_semester == id_curr_semester && x.jenis_skripsi_id == 1).ToList();
            ViewData["schedule_skripsi2"] = db.jadwal_semester.Where(x => x.id_semester == id_curr_semester && x.jenis_skripsi_id == 2).ToList();
            ViewData["semester_name"] = db.semesters.Where(x => x.id == id_curr_semester).ToList().First().periode_semester;

            return PartialView();
        }

        public bool changeSemester(string prevnext)
        {            
            bool success = true;
            var current = db.semesters.Where(x => x.isCurrent == 1).ToList();

            if (current.Count != 1) //semester table is empty, insert new row.
            {
                semester newsemester = new semester();
                newsemester.periode_semester = "Ganjil 2013/2014";
                newsemester.isCurrent = 1;
                db.semesters.Add(newsemester);
                try{
                    db.SaveChanges();
                }catch{
                    return false;
                }
                return true;
            }

            int id = current.First().id;
            if (prevnext == "prev")
            {
                var prev = db.semesters.Where(x => x.id == id - 1).ToList();
                if(prev.Count == 0) return false;

                current.First().isCurrent = 0;                
                prev.First().isCurrent = 1;
                db.Entry(current.First()).State = EntityState.Modified;
                db.Entry(prev.First()).State = EntityState.Modified;
            }
            else
            {
                var next = db.semesters.Where(x => x.id == id + 1).ToList();
                if(next.Count == 0) //no next semester in database. create new semester.
                {
                    semester newsemester = new semester();
                    newsemester.id = id + 1;
                    newsemester.periode_semester = this.nextSemesterName(current.First().periode_semester);
                    newsemester.isCurrent = 1;
                    db.semesters.Add(newsemester);

                    current.First().isCurrent = 0;
                    db.Entry(current.First()).State = EntityState.Modified;
                }
                else
                {
                    current.First().isCurrent = 0;                
                    next.First().isCurrent = 1;
                    db.Entry(current.First()).State = EntityState.Modified;
                    db.Entry(next.First()).State = EntityState.Modified;
                }
            }

            try
            {
                db.SaveChanges();
            }catch{
                return false;
            }
            return success;
        }

        public bool addSchedule(string event_name, int skripsi, string event_date)
        {            
            int id_current_semester = db.semesters.Where(x => x.isCurrent == 1).ToList().First().id;
            
            jadwal_semester jadwal = new jadwal_semester();
            jadwal.id_semester = id_current_semester;
            jadwal.tanggal = DateTime.ParseExact(event_date, "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);
            jadwal.isi = event_name;
            jadwal.jenis_skripsi_id = (byte)skripsi;

            db.jadwal_semester.Add(jadwal);
            try
            {
                db.SaveChanges();
            }
            catch 
            {
                return false;  
            }
            return true;
        }

        public bool removeSchedule(int jadwal_id)
        {
            jadwal_semester tobeDeleted = db.jadwal_semester.Where(x => x.id == jadwal_id).ToList().First();
            db.jadwal_semester.Remove(tobeDeleted);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private String nextSemesterName(String currentSemesterName)
        {
            System.Text.StringBuilder semester_name = new System.Text.StringBuilder() ;
            if (currentSemesterName.Contains("Ganjil"))
            {
                semester_name.Append("Genap ");
                semester_name.Append(currentSemesterName.Substring(currentSemesterName.IndexOf(' ') + 1));
            }
            else
            {
                semester_name.Append("Ganjil ");
                int slash = currentSemesterName.IndexOf('/');
                int year = Int32.Parse(currentSemesterName.Substring(slash + 1, 4));
                semester_name.Append(year.ToString());
                semester_name.Append("/");
                semester_name.Append((year + 1).ToString());
            }
            return semester_name.ToString();
        }
    }
}

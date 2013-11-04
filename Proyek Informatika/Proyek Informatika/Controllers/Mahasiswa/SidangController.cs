using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Proyek_Informatika.Controllers.Utilities;

namespace Proyek_Informatika.Controllers.Mahasiswa
{

    public class CalendarActionResponseModel
    {
        public String Status;
        public Int64 Source_id;
        public Int64 Target_id;

        public CalendarActionResponseModel(String status, Int64 source_id, Int64 target_id)
        {
            Status = status;
            Source_id = source_id;
            Target_id = target_id;
        }
    }

    public class SidangController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        private QueryController query = new QueryController();
        
        public ActionResult Pengumpulan()
        {
            string periode = db.semesters.Where(x=>x.isCurrent == 1).Select(y=>y.periode_semester).SingleOrDefault();
            int sessionSkripsi = (int)Int64.Parse(Session["id-skripsi"].ToString());
            int tipeId = db.skripsis.Where(x => x.id == sessionSkripsi).Select(y=>y.jenis).SingleOrDefault();
            string tipe;
            if (tipeId == 1)
            {
                tipe = "Presentasi";
            }
            else
            {
                tipe = "Akhir";
            }
            var semId = query.GetSemesterId(periode);
            var result = (from table in db.periode_sidang
                          where (table.semester_id == semId && table.tipe_sidang == tipe)
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

        

        public JsonResult Data()
        {
            string username = Session["username"].ToString();
            int sessionSkripsi = (int)Int64.Parse(Session["id-skripsi"].ToString());
            //events for loading to scheduler
            var items = from table in db.jadwal_tidak_kosong
                        where (table.username == (string) username && table.skripsi_id == sessionSkripsi)
                        select table;
            List<object> listResult = new List<object>();
            foreach (var result in items.ToList())
            {
                string pattern = "yyyy-MM-dd HH:mm:ss";
                string start_date = result.tanggal_mulai.ToString(pattern);
                string end_date = result.tanggal_selesai.ToString(pattern);

                listResult.Add(new
                {

                    result.id,
                    result.text,
                    start_date,
                    end_date,
                    description = result.description
                });
            }
            return Json(listResult);

        }




        public ActionResult Save(FormCollection actionValues)
        {
            String ids = actionValues["ids"];
            String action_type = actionValues[ids + "_!nativeeditor_status"];
            Int64 source_id = Int64.Parse(actionValues[ids + "_id"]);
            Int64 target_id = source_id;

            try
            {
                jadwal_tidak_kosong changedEvent = new jadwal_tidak_kosong();
                //changedEvent.id = (Int32)Int64.Parse(actionValues[ids + "_id"]);
                changedEvent.text = actionValues[ids + "_text"];
                changedEvent.tanggal_mulai = Convert.ToDateTime(actionValues[ids + "_start_date"]);
                changedEvent.tanggal_selesai = Convert.ToDateTime(actionValues[ids + "_end_date"]);
                changedEvent.description = actionValues[ids + "_description"];
                changedEvent.skripsi_id = (int)Int64.Parse(Session["id-skripsi"].ToString());
                switch (action_type)
                {
                    case "inserted":
                        changedEvent.username = Session["username"].ToString();
                        db.jadwal_tidak_kosong.Add(changedEvent);
                        break;
                    case "deleted":
                        changedEvent = db.jadwal_tidak_kosong.SingleOrDefault(ev => ev.id == source_id);
                        db.jadwal_tidak_kosong.Remove(changedEvent);
                        break;
                    default: // "updated"                          
                        changedEvent = db.jadwal_tidak_kosong.SingleOrDefault(ev => ev.id == source_id);
                        TryUpdateModel(changedEvent);
                        break;
                }

                db.SaveChanges();
                target_id = changedEvent.id;
            }
            catch (Exception a)
            {
                action_type = "error";
            }
            return View(new CalendarActionResponseModel(action_type, source_id, target_id));
        }

        public ActionResult Lihat()
        {
            return PartialView();
        }
    }
}

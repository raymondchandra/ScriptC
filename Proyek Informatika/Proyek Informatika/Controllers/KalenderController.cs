using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;

namespace Proyek_Informatika.Controllers
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

    public class KalenderController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        //
        // GET: /Kalender/

        public ActionResult Index()
        {
            return PartialView();
        }

        public JsonResult Data()
        {
            //var username = Session["username"];
            //events for loading to scheduler
            var items = from table in db.calendar_event
                        //where table.username == username
                        select table;
            List<object> listResult = new List<object>();
            foreach (var result in items.ToList())
            {
                string pattern = "yyyy-MM-dd HH:mm:ss";
                string start_date = result.start_date.ToString(pattern);
                string end_date = result.end_date.ToString(pattern);

                listResult.Add(new
                {

                    result.id,
                    result.text,
                    //result.place,
                    //result.priority,
                    //result.type,
                    result.start_date,
                    result.end_date,
                    result.description
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
                calendar_event changedEvent = new calendar_event();
                changedEvent.id = (Int32)Int64.Parse(actionValues[ids + "_id"]);
                changedEvent.text = actionValues[ids + "_text"];
                changedEvent.start_date = Convert.ToDateTime(actionValues[ids + "_start_date"]);
                changedEvent.end_date = Convert.ToDateTime(actionValues[ids + "_end_date"]);
                //changedEvent.place = actionValues[ids + "_place"];
                changedEvent.description = actionValues[ids + "_description"];
                //changedEvent.type = actionValues[ids + "_type"];
                //changedEvent.priority = actionValues[ids + "_priority"];
                
                switch (action_type)
                {
                    case "inserted":
                        changedEvent.username = "asdf";//(Int32)Int64.Parse(Session["id"].ToString());
                        db.calendar_event.Add(changedEvent);
                        break;
                    case "deleted":
                        changedEvent = db.calendar_event.SingleOrDefault(ev => ev.id == source_id);
                        db.calendar_event.Remove(changedEvent);
                        break;
                    default: // "updated"                          
                        changedEvent = db.calendar_event.SingleOrDefault(ev => ev.id == source_id);
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

    }
}

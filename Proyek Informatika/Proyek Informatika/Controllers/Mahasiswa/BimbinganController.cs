using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;

namespace Proyek_Informatika.Controllers.Mahasiswa
{
    
    public class BimbinganController : Controller
    {
        SkripsiAutoContainer db = new SkripsiAutoContainer();
        //
        // GET: /Bimbingan/

         public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pemesanan()
        {
            return PartialView();
        }
        public ActionResult KartuBimbingan()
        {
            return PartialView();
        }

        public ActionResult DetilBimbingan()
        {
            return PartialView();
        }

        
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult EditBimbingan(int id)
        {
            bimbingan result = db.bimbingans.Where<bimbingan>(x => x.id == id).Single<bimbingan>();
            TryUpdateModel<bimbingan>(result);
            int tipe = result.id_skripsi;
            db.SaveChanges();
            return bindingKartuBimbingan(tipe);
        }
        [GridAction]
        public ActionResult _SelectKartuBimbingan()
        {
            //int id_skripsi = 1;
            int id_skripsi = Int32.Parse(Session["id-skripsi"].ToString());
            return bindingKartuBimbingan(id_skripsi);
        }
        protected ViewResult bindingKartuBimbingan(int id_skripsi)
        {
            var result = (from bm in db.bimbingans
                          where bm.id_skripsi == id_skripsi
                          select new { id_skripsi = bm.id_skripsi, id = bm.id, isi = bm.isi, deskripsi = bm.deskripsi, tanggal = bm.tanggal });
            
            List<bimbingan> temp = new List<bimbingan>();

            foreach (var x in result)
            {
                temp.Add(new bimbingan() { id = x.id, id_skripsi = x.id_skripsi, deskripsi = x.deskripsi, isi = x.isi, tanggal = x.tanggal });
            }
            return View(new GridModel<bimbingan>{ Data = temp});
        }


        #region kalender
        public JsonResult Data()
        {
            var username = Session["username"].ToString();
            var npm = db.mahasiswas.Where(x => x.username == username).Select(y => y.NPM).SingleOrDefault();
            //events for loading to scheduler
            var items = from table in db.pesanan_bimbingan
                        where table.NPM_mahasiswa == npm
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
                    result.description,
                    //result.NIK_dosen,
                    result.setuju,
                    start_date,
                    end_date
                });
            }
            return Json(listResult);

        }




        public ActionResult Save(FormCollection actionValues,bool editing)
        {
            String ids = actionValues["ids"];
            String action_type = actionValues[ids + "_!nativeeditor_status"];
            Int64 source_id = Int64.Parse(actionValues[ids + "_id"]);
            Int64 target_id = source_id;

            pesanan_bimbingan changedEvent = new pesanan_bimbingan();
            try
            {
                
                switch (action_type)
                {
                    case "inserted":
                       
                        changedEvent.text = actionValues[ids + "_text"];
                        changedEvent.tanggal_mulai = Convert.ToDateTime(actionValues[ids + "_start_date"]);
                        changedEvent.tanggal_selesai = Convert.ToDateTime(actionValues[ids + "_end_date"]);
                        changedEvent.description = actionValues[ids + "_description"];var username = Session["username"].ToString();
                        var npm = db.mahasiswas.Where(x => x.username == username).Select(y => y.NPM).SingleOrDefault();
                        var nik = db.skripsis.Where(x => x.NPM_mahasiswa == npm).Select(y => y.NIK_dosen_pembimbing).SingleOrDefault();
                        changedEvent.NPM_mahasiswa = npm;
                        changedEvent.NIK_dosen = nik;
                        changedEvent.setuju = "menunggu";
                        db.pesanan_bimbingan.Add(changedEvent);
                        break;
                    case "deleted":
                        changedEvent = db.pesanan_bimbingan.SingleOrDefault(ev => ev.id == source_id);
                        db.pesanan_bimbingan.Remove(changedEvent);
                        break;
                    default: // "updated"                          
                        changedEvent = db.pesanan_bimbingan.SingleOrDefault(ev => ev.id == source_id);
                        changedEvent.text = actionValues[ids + "_text"];
                        changedEvent.tanggal_mulai = Convert.ToDateTime(actionValues[ids + "_start_date"]);
                        changedEvent.tanggal_selesai = Convert.ToDateTime(actionValues[ids + "_end_date"]);
                        changedEvent.setuju = "menunggu";
                        changedEvent.description = actionValues[ids + "_description"];
                        break;
                }

                db.SaveChanges();
                target_id = changedEvent.id;
            }
            catch
            {
                action_type = "error";
            }
            return View(new CalendarActionResponseModel(action_type, source_id, target_id));
        }

        #endregion
    }
}

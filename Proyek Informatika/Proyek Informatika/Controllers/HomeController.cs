using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;
using System.Data;

namespace Proyek_Informatika.Controllers
{
    public class HomeController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();

        #region view
        public ActionResult Index()
        {
            List<int> temp = (from s in db.semesters
                              select s.id).ToList();
            int max = temp.Max();
            semester x = db.semesters.Where(semesterTemp => semesterTemp.id == max).SingleOrDefault();
            Session["id-semester"] = x.id;
            Session["semester"] = x.periode_semester;
            return View();
        }

        public ActionResult About()
        {
            return PartialView();
        }

        public ActionResult Topik()
        {
            return PartialView();
        }

        public ActionResult TopikAdmin()
        {
            return PartialView();
        }

        public ActionResult TopikInsert()
        {
            return PartialView();
        }
        public ActionResult TopikUpdate()
        {
            return PartialView();
        }
        #endregion

        #region grid topik

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult TopikInsert(TopikView model)
        {
            string username = (string)Session["username"];
            var dosen = db.dosens.Where(dosenTemp => dosenTemp.username == username).SingleOrDefault();
            topik t = new topik();
            t.NIK_pembimbing = dosen.NIK;
            t.judul = model.judul;
            t.deskripsi = model.deskripsi;
            t.keterangan = "tersedia";
            t.id_semester = int.Parse(Session["id-semester"].ToString());
            if (TryUpdateModel(t))
            {
                db.topiks.Add(t);
                db.SaveChanges();
            }
            Session["redirect"] = "topik";
            Session["message"] = "insert";
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult TopikUpdate(TopikView model)
        {
            var t = db.topiks.Where(topikTemp => topikTemp.id == model.id).SingleOrDefault();
            t.judul = model.judul;
            t.deskripsi = model.deskripsi;
            t.keterangan = model.keterangan;
            TryUpdateModel(t);
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
            Session["redirect"] = "topik";
            Session["message"] = "update";
            return RedirectToAction("Index", "Home");
        }

        [GridAction]
        public ActionResult _SelectTopikView()
        {
            return bindingTable();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteTopikView(int id)
        {
            var t = db.topiks.Where(topikTemp => topikTemp.id == id).SingleOrDefault();
            db.topiks.Remove(t);
            db.SaveChanges();
            return bindingTable();
        }

        protected ViewResult bindingTable()
        {
            int idSemester = int.Parse(Session["id-semester"].ToString());
            var login = (string)Session["role"];
            var username = (string)Session["username"];
            var temp = (from t in db.topiks
                        join d in db.dosens on t.NIK_pembimbing equals d.NIK
                        where t.id_semester == idSemester
                        select new { t.id, t.judul, t.deskripsi, t.keterangan, d.nama, d.username }).ToList();

            List<TopikView> listResult = new List<TopikView>();
            foreach (var t in temp)
            {
                TopikView x = new TopikView
                {
                    id = t.id,
                    judul = t.judul,
                    deskripsi = t.deskripsi,
                    keterangan = t.keterangan,
                    pembimbing = t.nama
                };
                if (login == "dosen")
                {
                    if (username == t.username)
                    {
                        listResult.Add(x);
                    }

                }
                else
                {
                    listResult.Add(x);
                }

            }

            return View(new GridModel<TopikView>
            {
                Data = listResult
            });



            //if ((string)Session["role"] == "koordinator")
            //{
            //    var listResult = (from table in db.topiks
            //                      select table).ToList();
            //    return View(new GridModel<topik>
            //    {
            //        Data = listResult
            //    });
            //}
            //else if ((string)Session["role"] == "dosen")
            //{
            //string username = (string)Session["username"];
            //string nik = (db.dosens.Where(dosenTemp => dosenTemp.username == username).SingleOrDefault()).NIK;


        }



        //}
        //else
        //{                              
        //    var listResult = (from table in db.topiks
        //                        where (table.keterangan != "belum disetujui")
        //                        select table).ToList();
        //    return View(new GridModel<topik>
        //    {
        //        Data = listResult
        //    });
        //}

    }
        #endregion

}


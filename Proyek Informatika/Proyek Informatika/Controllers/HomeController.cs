using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;
using System.Data;
using Proyek_Informatika.Controllers.Utilities;

namespace Proyek_Informatika.Controllers
{
    public class HomeController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();

        [AcceptVerbs(HttpVerbs.Post)]
        public string SetIdSemester()
        {
            semester x = db.semesters.Where(semesterTemp => semesterTemp.isCurrent == 1).SingleOrDefault();
            Session["id-semester"] = x.id;
            return x.periode_semester;
        }

        #region view
        public ActionResult Index()
        {
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

        public ActionResult TopikUpdate()
        {
            return PartialView();
        }
        #endregion

        #region grid topik

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public string _InsertTopik(TopikContainer model)
        {
            //validasi
            if (model.judul == null || model.judul == "")
            {
                return "Registrasi topik gagal! \nField judul harus diisi!";
            }
            var temp = db.topiks.Where(t => t.judul == model.judul).SingleOrDefault();
            if (temp != null)
            {
                return "Registrasi topik gagal! \nAda topik lain dengan judul yang sama!";
            }

            //insert
            topik tpk = new topik();
            string username = (string)Session["username"];
            tpk.NIK_pembimbing = (db.dosens.Where(dosenTemp => dosenTemp.username == username).First()).NIK;
            tpk.judul = model.judul;
            tpk.deskripsi = model.deskripsi;
            tpk.keterangan = "tersedia";
            tpk.id_semester = int.Parse(Session["id-semester"].ToString());
            if (TryUpdateModel(tpk))
            {
                db.topiks.Add(tpk);
                db.SaveChanges();
            }
            return "Registrasi topik berhasil!";
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public string _UpdateTopik(TopikContainer model)
        {
            //validasi
            if (model.judul == null || model.judul == "")
            {
                return "Registrasi topik gagal! \nField judul harus diisi!";
            }
            var temp = db.topiks.Where(t => t.judul == model.judul).SingleOrDefault();
            if (temp != null && temp.id != model.id)
            {
                return "Edit topik gagal! \nAda topik lain dengna judul yang sama!";
            }

            //update
            topik tpk = db.topiks.Where(topikTemp => topikTemp.id == model.id).First();
            tpk.judul = model.judul;
            tpk.deskripsi = model.deskripsi;
            if (TryUpdateModel(tpk))
            {
                //db.topiks.Add(topik);
                db.SaveChanges();
            }
            return "Edit topik berhasil!";
        }

        [GridAction]
        public ActionResult _SelectTopik()
        {
            return bindingTable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteTopik(int id)
        {
            var t = db.topiks.Where(topikTemp => topikTemp.id == id).First();
            db.topiks.Remove(t);
            db.SaveChanges();
            return bindingTable();
        }

        protected ViewResult bindingTable()
        {
            int idSemester = int.Parse(Session["id-semester"].ToString());
            string role = (string)Session["role"];
            string username = (string)Session["username"];

            List<TopikContainer> listResult;
            if (role == null || role == "")
            {
                listResult = (from t in db.topiks
                              join d in db.dosens on t.NIK_pembimbing equals d.NIK
                              where t.id_semester == idSemester
                              select new TopikContainer { id = t.id, judul = t.judul, deskripsi = t.deskripsi, keterangan = t.keterangan, pembimbing = d.nama }).ToList();
            }
            else if (role.ToLower().Equals("dosen"))
            {
                listResult = (from t in db.topiks
                              join d in db.dosens on t.NIK_pembimbing equals d.NIK
                              where t.id_semester == idSemester
                              where t.keterangan == "tersedia"
                              where d.username == username
                              select new TopikContainer { id = t.id, judul = t.judul, deskripsi = t.deskripsi, keterangan = t.keterangan, pembimbing = d.nama }).ToList();
            }
            else //koordinator
            {
                listResult = (from t in db.topiks
                              join d in db.dosens on t.NIK_pembimbing equals d.NIK
                              where t.id_semester == idSemester
                              where t.keterangan == "tersedia"
                              select new TopikContainer { id = t.id, judul = t.judul, deskripsi = t.deskripsi, keterangan = t.keterangan, pembimbing = d.nama }).ToList();
            }


            return View(new GridModel<TopikContainer>
            {
                Data = listResult
            });
        }
    }
        #endregion

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class PengumumanDosenController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        private HomeController hc = new HomeController();

        #region view
        //
        // GET: /BlokPengumuman/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pengumuman()
        {
            return PartialView();
        }

        public ActionResult BuatPengumuman()
        {
            return PartialView();
        }

        public ActionResult EditPengumuman()
        {
            return PartialView();
        }

        public ActionResult EditPengumumanWindow()
        {
            return PartialView();
        }
        #endregion

        #region pengumuman

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public string InsertPengumuman(PengumumanContainer model)
        {
            //validasi
            if (model.judul == null || model.judul == "")
            {
                return "Pengumuman gagal ditambahkan! \nField judul harus diisi!";
            }
            if (model.isi == null || model.isi == "")
            {
                return "Pengumuman gagal ditambahkan! \nField konten pengumuman harus diisi!";
            }
            pengumuman p = new pengumuman
            {
                tanggal = DateTime.Now,
                isi = model.isi,
                target = model.target,
                judul = model.judul,
                pembuat = (string)Session["username"]
            };

            if (TryUpdateModel(p))
            {
                db.pengumumen.Add(p);
                db.SaveChanges();
            }
            return "Pengumuman berhasil ditambahkan.";
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string SelectPengumuman()
        {
            string username = (string)Session["username"];
            int[] array = { 8, 9, 10, 11, 12, 13, 14, 15 };
            List<PengumumanContainer> listResult = (from p in db.pengumumen
                                                    where (p.pembuat.ToLower() == "admin" || p.pembuat.ToLower() == username.ToLower())
                                                    orderby p.tanggal descending
                                                    select new PengumumanContainer { id = p.id, target = p.target, judul = p.judul, isi = p.isi, pembuat = p.pembuat, tanggal = p.tanggal }).ToList();

            string result = "";
            foreach (PengumumanContainer pc in listResult)
            {
                if (pc.pembuat.ToLower() == "admin")
                {
                    if ((Array.IndexOf(array, pc.target) != -1))
                    {
                        result += hc.format(pc);
                    }
                }
                else
                {
                    result += hc.format(pc);
                }
            }
            return result;
        }

        #endregion

        #region grid pengumuman

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public string _UpdatePengumuman(PengumumanContainer model)
        {
            //validasi
            if (model.judul == null || model.judul == "")
            {
                return "Pengumuman gagal diubah! \nField judul harus diisi!";
            }
            if (model.isi == null || model.isi == "")
            {
                return "Pengumuman gagal diubah! \nField konten pengumuman harus diisi!";
            }
            pengumuman p = db.pengumumen.Where(pengumumanTemp => pengumumanTemp.id == model.id).First();
            p.tanggal = DateTime.Now;
            p.isi = model.isi;
            p.target = model.target;
            p.judul = model.judul;

            if (TryUpdateModel(p))
            {
                db.SaveChanges();
            }
            return "Pengumuman berhasil diubah.";
        }

        [GridAction]
        public ActionResult _SelectPengumuman()
        {
            return bindingTable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeletePengumuman(int id)
        {
            var p = db.pengumumen.Where(pengumumanTemp => pengumumanTemp.id == id).First();
            db.pengumumen.Remove(p);
            db.SaveChanges();
            return bindingTable();
        }

        protected ViewResult bindingTable()
        {
            string username = (string)Session["username"];
            List<PengumumanContainer> listResult = (from p in db.pengumumen
                                                    where (p.pembuat.ToLower() == username.ToLower())
                                                    orderby p.tanggal descending
                                                    select new PengumumanContainer { id = p.id, target = p.target, judul = p.judul, isi = p.isi, pembuat = p.pembuat, tanggal = p.tanggal }).ToList();

            return View(new GridModel<PengumumanContainer>
            {
                Data = listResult
            });
        }

        #endregion



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class PendaftaranDosenController : Controller
    {
        //
        // GET: /Pengumpulan/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrasiMahasiswa()
        {
            return PartialView();
        }


        protected ViewResult bindingTopikMahasiswa(int id)
        {

            List<TopikMahasiswa> temp = new List<TopikMahasiswa>();

            TopikMahasiswa x = new TopikMahasiswa()
            {
                NPM = 201073221,
                Nama = "Regina Puspa Rani",
                Topik = "Pembangunan Perangkat Lunak Sistem Akuntansi Pembukuan dengan Pendekatan Prosedural",
            }; temp.Add(x);

            x = new TopikMahasiswa()
            {
                NPM = 200973190,
                Nama = "Clinton Gunawan",
                Topik = "Perancangan Aplikasi E-commerce Berbasis Mobile",
            }; temp.Add(x);

            return View(new GridModel<TopikMahasiswa>
            {
                Data = temp
            });
        }
        [GridAction]
        public ActionResult _SelectTopikMahasiswa()
        {
            return bindingTopikMahasiswa(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveTopikMahasiswa(int id)
        {

            return bindingTopikMahasiswa(id);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertTopikMahasiswa()
        {

            return bindingTopikMahasiswa(2);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteTopikMahasiswa(int id)
        {

            return bindingTopikMahasiswa(id);
        }

    }
}
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
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
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


        #region grid mahasiswa
        [GridAction]
        public ActionResult _SelectMahasiswa()
        {
            return bindingTable();
        }

       

        protected ViewResult bindingTable()
        {
            var listResult = (from table in db.mahasiswas
                              
                              select table).ToList();
            var t = listResult.ElementAt(0);
            var a = t.NPM;
         

            return View(new GridModel<mahasiswa>
            {
                Data = listResult
            });
        }
        #endregion

    }
}
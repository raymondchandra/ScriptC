using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyek_Informatika.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        #region Mahasiswa

        public ActionResult BiodataMahasiswa()
        {
            return PartialView();
        }

        public ActionResult SejarahMahasiswa()
        {
            return PartialView();
        }

        #endregion

        #region Dosen
        public ActionResult BiodataDosen()
        {
            return PartialView();
        }

        public ActionResult SejarahDosen()
        {
            return PartialView();
        }

        public ActionResult PengaturanDosen()
        {
            return PartialView();
        }
        #endregion

        public ActionResult Pengaturan()
        {
            return PartialView();
        }

        public ActionResult ListDosen()
        {
            return PartialView();
        }

        public ActionResult ListMahasiswa()
        {
            return PartialView();
        }

        public ActionResult Registrasi()
        {
            return PartialView();
        }
    }
}

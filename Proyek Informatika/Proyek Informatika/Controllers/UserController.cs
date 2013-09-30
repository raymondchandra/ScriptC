using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
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

        public ActionResult Skripsi1()
        {
            return PartialView();
        }
        public ActionResult Skripsi2()
        {
            return PartialView();
        }

        public ActionResult Registrasi()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult _SelectMahasiswa( )
        {
            return bindingListMahasiswa(0);
        }
        public ViewResult bindingListMahasiswa(int id)
        {
            List<ListMahasiswa> temp;

            temp = new List<ListMahasiswa>();

            temp.Add(new ListMahasiswa()
            {
                id = 1,
                judul = "JST",
                namaMahasiswa = "Hanzolo",
                npm = "2011730050"
            });

            temp.Add(new ListMahasiswa()
            {
                id = 3,
                judul = "JST",
                namaMahasiswa = "Batman",
                npm = "2011730080"
            });
            return View(new GridModel<ListMahasiswa>() { Data = temp });
        }
        
    }
}

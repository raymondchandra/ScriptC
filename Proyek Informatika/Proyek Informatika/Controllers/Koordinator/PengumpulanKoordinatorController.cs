using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
namespace Proyek_Informatika.Controllers.Koordinator
{
    public class PengumpulanKoordinatorController : Controller
    {
        //
        // GET: /Pengumpulan/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PengumpulanFile()
        {
            return PartialView();
        }
        public ActionResult PreviewDocument()
        {
            return PartialView();
        }
        
        [GridAction]
        public ActionResult _SelectPengumpulanFile()
        {
            return bindingPengumpulanFile(0);
        }
        protected ViewResult bindingPengumpulanFile(int id)
        {
            List<KoordinatorPengumpulanFile> temp;

            temp = new List<KoordinatorPengumpulanFile>();

            temp.Add(new KoordinatorPengumpulanFile()
            {
                id = 1,
                npmMahasiswa = "2010730125",
                namaMahasiswa ="Hanzolo",
                judul ="JST",
                waktuKumpul = "19/08/2012",
                dokumen ="Kontrak Kerja"
            });
            return View(new GridModel<KoordinatorPengumpulanFile>() { Data = temp });
        }


    }
}

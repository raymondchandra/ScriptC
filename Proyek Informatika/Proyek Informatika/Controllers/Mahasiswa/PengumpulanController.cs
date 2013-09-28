using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
using System.IO;

namespace Proyek_Informatika.Controllers.Mahasiswa
{
    public class PengumpulanController : Controller
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

        [GridAction]
        public ActionResult _SelectPengumpulan()
        {
            return bindingPengumpulan();
        }
        public ViewResult bindingPengumpulan()
        {
            List<MahasiswaPengumpulanFile> temp;

            temp = new List<MahasiswaPengumpulanFile>();

            temp.Add(new MahasiswaPengumpulanFile()
            {
                id = 1,
                dokumen = "Kontrak Kerja",
                waktuKumpul = "30/10/2012"
            });

            temp.Add(new MahasiswaPengumpulanFile()
            {
                id = 1,
                dokumen = "Kontrak Kerja",
                waktuKumpul = "2/11/2012"
            });
            temp.Add(new MahasiswaPengumpulanFile()
            {
                id = 1,
                dokumen = "Laporan 1",
                waktuKumpul = "16/11/2012"
            });
            temp.Add(new MahasiswaPengumpulanFile()
            {
                id = 1,
                dokumen = "Laporan 2",
                waktuKumpul = "5/12/2012"
            });

            return View(new GridModel<MahasiswaPengumpulanFile> { Data = temp });
        }

        //Pengumpulan file
        [HttpPost]
        public ActionResult ProcessFile(IEnumerable<HttpPostedFileBase> attachments)
        {
            if (attachments != null)
            {
                TempData["UploadedAttachments"] = GetFileInfo(attachments);
            }
            return RedirectToAction("SyncUploadResult");
        }
        private IEnumerable<string> GetFileInfo(IEnumerable<HttpPostedFileBase> attachments)
        {
            return
                from a in attachments
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        }

    }
}

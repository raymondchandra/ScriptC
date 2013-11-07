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
        SkripsiAutoContainer db = new SkripsiAutoContainer();
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
            return bindingPengumpulanFile();
        }
        protected ViewResult bindingPengumpulanFile()
        {
            List<KoordinatorPengumpulanContainer> result = (from si in db.skripsis
                          join mh in db.mahasiswas on si.NPM_mahasiswa equals mh.NPM
                          join ds in db.dosens on si.NIK_dosen_pembimbing equals ds.NIK
                          join fl in db.laporans on si.id equals fl.id_skripsi
                          orderby mh.NPM ascending
                          select new KoordinatorPengumpulanContainer()
                          {
                              id = fl.id,
                              npmMahasiswa = mh.NPM,
                              namaMahasiswa = mh.nama,
                              pembimbing = ds.nama,
                              dokumen = fl.nama_file,
                              waktuKumpul = fl.tanggal_pengumpulan,
                              judul = si.topik.judul,
                              deskripsi = fl.deskripsi,
                              skripsi = si.jenis_skripsi.nama_jenis
                          }).ToList<KoordinatorPengumpulanContainer>();

            
            return View(new GridModel<KoordinatorPengumpulanContainer>() { Data = result });
        }


    }
}

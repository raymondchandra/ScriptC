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
            HansContainer.EnumSemesterSkripsiContainer semes = new HansContainer.EnumSemesterSkripsiContainer();
            var result = from jn in db.jenis_skripsi
                         select (new { id = jn.id, nama_jenis = jn.nama_jenis });
            List<jenis_skripsi> temp = new List<jenis_skripsi>();
            foreach (var x in result)
            {
                temp.Add(new jenis_skripsi { id = x.id, nama_jenis = x.nama_jenis });
            }
            semes.jenis_skripsi = temp;

            List<semester> temp2 = new List<semester>();
            var result2 = from si in db.semesters
                          select (new { id = si.id, nama = si.periode_semester, curr = si.isCurrent });
            foreach (var x in result2)
            {
                temp2.Add(new semester { id = x.id, periode_semester = x.nama, isCurrent = x.curr });
                if (x.curr == 1)
                {
                    semes.selected_value = x.id;
                }
            }
            semes.jenis_semester = temp2;
            return PartialView(semes);
        }
        [HttpPost]
        public ActionResult FileMahasiswa(int periode, int jenis_skripsi)
        {
            ViewBag.periode = periode;
            ViewBag.jenis_skripsi = jenis_skripsi;
            return PartialView();
        }
        [GridAction]
        public ActionResult _SelectPengumpulanFile(int periode, int jenis_skripsi)
        {
            return bindingPengumpulanFile(periode, jenis_skripsi);
        }
        protected ViewResult bindingPengumpulanFile(int periode, int jenis_skripsi)
        {
            List<HansContainer.KoordinatorPengumpulanContainer> result = (from si in db.skripsis
                          join mh in db.mahasiswas on si.NPM_mahasiswa equals mh.NPM
                          join ds in db.dosens on si.NIK_dosen_pembimbing equals ds.NIK
                          join fl in db.laporans on si.id equals fl.id_skripsi
                          orderby mh.NPM ascending
                          select new HansContainer.KoordinatorPengumpulanContainer()
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
                          }).ToList<HansContainer.KoordinatorPengumpulanContainer>();

            
            return View(new GridModel<HansContainer.KoordinatorPengumpulanContainer>() { Data = result });
        }


    }
}

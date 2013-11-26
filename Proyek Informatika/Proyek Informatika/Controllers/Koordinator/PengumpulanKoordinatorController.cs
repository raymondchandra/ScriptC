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
            EnumSemesterSkripsiContainer semes = new EnumSemesterSkripsiContainer();
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
        public ActionResult FileMahasiswa(int periode=0, int jenis_skripsi=0)
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
            List<KoordinatorPengumpulanContainer> result = (from si in db.laporans
                         where si.skripsi.jenis == jenis_skripsi && si.skripsi.id_semester_pengambilan == periode
                         select new KoordinatorPengumpulanContainer() {
                             id= si.id,
                             dokumen = si.nama_file,
                             deskripsi = si.deskripsi,
                             waktuKumpul = si.tanggal_pengumpulan,
                             
                             judul = si.skripsi.topik.judul,
                             pembimbing = si.skripsi.dosen.nama,
                             namaMahasiswa = si.skripsi.mahasiswa.nama,
                             nikDosen = si.skripsi.dosen.NIK,
                             npmMahasiswa = si.skripsi.mahasiswa.NPM,
                             skripsi = si.skripsi.jenis_skripsi.nama_jenis
                             }).ToList();

            
            return View(new GridModel<KoordinatorPengumpulanContainer>() { Data = result });
        }


    }
}

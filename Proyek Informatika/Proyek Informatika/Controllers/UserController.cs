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









        public ActionResult DaftarMahasiswa()
        {
            return PartialView();
        }

        protected ViewResult bindingMahasiswa(int id)
        {

            List<MahasiswaPeserta> temp = new List<MahasiswaPeserta>();

            MahasiswaPeserta x = new MahasiswaPeserta()
            {
                foto = "C:/something/pic.jpg",
                NPM = "2010730001",
                nama = "Raymond Chandra",
                email = "raymond@chibi.unyu",
                semester = "Ganjil 2013/2014",
                status = "gencat",
                topik = "Algoritma Baru Untuk Menghitung Graf Dalam Ruang Hampa",
                pengambilan_ke = 2,
                dosen = "Lionov",
                nilai_akhir = 70,
            };
            temp.Add(x);
            x = new MahasiswaPeserta()
            {
                foto = "C:/something/pic2.jpg",
                NPM = "2010730002",
                nama = "Chintya Dewi",
                email = "chintya@chibi.unyu",
                semester = "Ganjil 2013/2014",
                status = "ambil bersama Skripsi 1 & Skripsi 2",
                topik = "Implementasi Komputasi Paralel Untuk Menghitung Luas Tata Surya",
                pengambilan_ke = 1,
                dosen = "Lionov",
                nilai_akhir = 100,
            };
            temp.Add(x);

            return View(new GridModel<MahasiswaPeserta>
            {
                Data = temp
            });
        }

        [GridAction]
        public ActionResult _SelectMahasiswaKoor()
        {
            return bindingMahasiswa(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveMahasiswa(int id)
        {

            return bindingMahasiswa(id);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertMahasiswa()
        {
            return bindingMahasiswa(2);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteMahasiswa(int id)
        {

            return bindingMahasiswa(id);
        }


        public ActionResult DaftarDosen()
        {
            return PartialView();
        }

        protected ViewResult bindingDosen(int id)
        {
            List<DosenPembimbing> listDosen = new List<DosenPembimbing>();

            DosenPembimbing x = new DosenPembimbing()
            {
                NIK = "1234567",
                nama = "Lionov",
                email = "lionov@gmail.com",
                NPM_mhs = "2010730001",
                nama_mhs = "Raymond Chandra",
                topik = "Algoritma Baru Untuk Menghitung Graf Dalam Ruang Hampa",
                status = "Skripsi 1"
            };
            listDosen.Add(x);
            x = new DosenPembimbing()
            {
                NIK = "1234567",
                nama = "Lionov",
                email = "lionov@gmail.com",
                NPM_mhs = "2010730002",
                nama_mhs = "Chintya Dewi",
                topik = "Implementasi Komputasi Paralel Untuk Menghitung Luas Tata Surya",
                status = "ambil bersama Skripsi 1 & Skripsi 2"
            };
            listDosen.Add(x);
            x = new DosenPembimbing()
            {
                NIK = "1234567",
                nama = "Lionov",
                email = "lionov@gmail.com",
                NPM_mhs = "2010730089",
                nama_mhs = "Albertus Alvin",
                topik = "Pengobatan Kanker Dengan Algoritma Djikstra",
                status = "Skripsi 2"
            };
            listDosen.Add(x);
            x = new DosenPembimbing()
            {
                NIK = "9876543",
                nama = "Thomas Anung",
                email = "tanung@yahoo.com",
                NPM_mhs = "2010730003",
                nama_mhs = "Hans W. Halim",
                topik = "Program Simulasi Pesawat Terbang Menggunakan Bahasa Assembly",
                status = "ambil bersama Skripsi 1 & Skripsi 2"
            };
            listDosen.Add(x);

            return View(new GridModel<DosenPembimbing>
            {
                Data = listDosen
            });
        }

        [GridAction]
        public ActionResult _SelectDosen()
        {
            return bindingDosen(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveDosen(int id)
        {

            return bindingDosen(id);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertDosen()
        {
            return bindingDosen(2);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteDosen(int id)
        {

            return bindingDosen(id);
        }

        

        
        
    }
}

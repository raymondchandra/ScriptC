using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
using System.IO;
using Proyek_Informatika.Controllers;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using Proyek_Informatika.Utilities;


namespace Proyek_Informatika.Controllers.Mahasiswa
{
    public class PengumpulanController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();

        //
        // GET: /Pengumpulan/

        public ActionResult Index()
        {
            return View();
        }
        #region ui
        public ActionResult FormPengumpulan()
        {
            return PartialView();
        }
        public ActionResult EditorPengumpulan(int idLaporan)
        {
            var result = db.laporans.Where(x => x.id == idLaporan).Single<laporan>();
            laporan temp = new laporan();
            temp.id = result.id;
            temp.id_skripsi = result.id_skripsi;
            temp.jenis = result.jenis;
            temp.deskripsi = result.deskripsi;
            temp.nama_file = result.nama_file;
            temp.tanggal_pengumpulan = result.tanggal_pengumpulan;
            return PartialView(temp);
        }
        public string DeleteFile(int id_laporan)
        {
            laporan result = db.laporans.Where<laporan>(x => x.id == id_laporan).Single<laporan>();
            string file = result.nama_file;
            db.laporans.Remove(result);
            db.SaveChanges();
            return file;
        }
        public ActionResult PengumpulanFile()
        {
            ViewBag.username = Session["username"];
            return PartialView();
        }
        
        public ActionResult GetDownload(int id_laporan)
        {
            //ambil filename di database
            var result = db.laporans.Where<laporan>(x => x.id == id_laporan).Single();
            int id_skripsi = result.id_skripsi;
            var username = (from si in db.skripsis
                            join mh in db.mahasiswas on si.NPM_mahasiswa equals mh.NPM
                            where si.id == id_skripsi
                            select new { username = mh.username }).Single();
            string filename = result.nama_file;
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Upload/File Mahasiswa/"+username.username+"/dokumen/" + filename));
                bool exist = System.IO.File.Exists(Server.MapPath("~/Upload/File Mahasiswa/" + username.username+ "/dokumen/" + filename));
                if (!exist)
                {
                    return null;
                }

                string fileType = getFileType(filename);
                return File(fs, fileType, filename);
            }
            catch
            {
                return null;
                //throw new HttpException(404, "Couldn't find " + filename);
            }
        }
        
        public ActionResult PDFViewer(int id_laporan)
        {
            var result = db.laporans.Where<laporan>(x => x.id == id_laporan).ToList();
            int id_skripsi = result.ElementAt<laporan>(0).id_skripsi;
            var username = (from si in db.skripsis
                           join mh in db.mahasiswas on si.NPM_mahasiswa equals mh.NPM
                           where si.id == id_skripsi
                           select new { username = mh.username }).Single();
            if (result.Count != 0)
            {
                var get = result.First();
                string file = Server.MapPath(Url.Content("~/Upload/File Mahasiswa/"+username.username+"/dokumen/" + result.ElementAt<laporan>(0).nama_file));
                
                try
                {
                    
                    PdfReader reader = new PdfReader(file);
                    MemoryStream pdfStream = new MemoryStream();

                    PdfStamper pdfStamper = new PdfStamper(reader, pdfStream);

                    reader.Close();
                    pdfStamper.Close();
                    pdfStream.Flush();
                    pdfStream.Close();

                    byte[] pdfArray = pdfStream.ToArray();
                    
                    return new BinaryContentResult(pdfArray, "application/pdf");
                     
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
         
        #endregion

        #region upload
        public int SubmitFile(string jenis, string deskripsi, string name)
        {
            int id_skripsi;

            id_skripsi = -1;
            if (Session["id-skripsi"] != null)
            {
                id_skripsi = Int32.Parse(Session["id-skripsi"].ToString());

            }
            string id_pengambilan = Session["id-semester"].ToString();
            Console.WriteLine(id_pengambilan);
            laporan temp = new laporan();

            temp.jenis = jenis;
            temp.deskripsi = deskripsi;
            temp.id_skripsi = id_skripsi;
            temp.nama_file = name;
            temp.tanggal_pengumpulan = DateTime.Now;
            try
            {
                db.laporans.Add(temp);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
                
            }
        }
        public int EditFile(int id_laporan, string jenis, string deskripsi, string name)
        {
            string tempName;
            
            var result = db.laporans.Where<laporan>(x => x.id == id_laporan).Single();
            tempName = result.nama_file;
            
            result.jenis = jenis;
            result.deskripsi = deskripsi;
            result.nama_file = name;
            try
            {
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;

            }
        }
        [HttpPost]
        public string Unggah(laporan laporan)
        {
            int id_skripsi;
            if (Session["id-skripsi"] == null)
            {
                return null;
            }
            id_skripsi = (Int32)Int64.Parse(Session["id-skripsi"].ToString());
            string username = Session["username"].ToString();
            laporan.id_skripsi = id_skripsi;
            String deskripsi = laporan.deskripsi;
            laporan.tanggal_pengumpulan = DateTime.Now;
            db.laporans.Add(laporan);
            db.SaveChanges();
            return "berhasil";
        }

        #endregion
       


        #region select

        [GridAction]
        public ActionResult SelectPengumpulan()
        {
            int id_skripsi = -1;
            if (Session["id-skripsi"] != null)
            {
                id_skripsi = Int32.Parse(Session["id-skripsi"].ToString());
            }
            return bindingPengumpulan(id_skripsi);

        }
        public ViewResult bindingPengumpulan(int id_skripsi)
        {
            var result = db.laporans.Where<laporan>(x => x.id_skripsi == id_skripsi);
            //List<laporan> temp = db.laporans.Where<laporan>(x => x.id_skripsi == id_skripsi).Select(x => new laporan()
            //{
            //    id = x.id,
            //    id_skripsi = x.id_skripsi,
            //    jenis = x.jenis,
            //    deskripsi = x.deskripsi
            //}).ToList();
                          
            //List<laporan> temp = result.ToList<laporan>();
            List<laporan> temp = new List<laporan>();

            foreach (var i in result)
            {
                laporan temp2 = new laporan();
                temp2.id = i.id;
                temp2.deskripsi = i.deskripsi;
                temp2.jenis = i.jenis;
                temp2.id_skripsi = i.id_skripsi;
                temp.Add(temp2);
            }

            return View(new GridModel<laporan> { Data = temp });
        }
        #endregion
        public static string getFileType(string filename)
        {
            string[] temp = filename.Split('.');
            switch (temp.Last())
            {
                case "pdf":
                    return "application/pdf";
                case "xls":
                    return "application/msexcel";
                case "xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                default:
                    return "application/octet-stream";
            };
        }
    }
}

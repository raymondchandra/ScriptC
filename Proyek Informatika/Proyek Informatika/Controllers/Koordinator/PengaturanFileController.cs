using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

namespace Proyek_Informatika.Controllers.Koordinator
{
    public class PengaturanFileController : Controller
    {
        //
        // GET: /PengaturanFile/
        SkripsiAutoContainer db = new SkripsiAutoContainer();

        public ActionResult Index()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult SelectFile()
        {
            return bindingFile();
        }
        protected ViewResult bindingFile()
        {
            var result = db.files;
            List<file> temp = result.ToList<file>();

            return View(new GridModel<file>() { Data = temp});
        }
        
        public ActionResult Form()
        {
            ViewBag.path = "";
            try
            {

                file result = db.files.Where(x => x.nama_file.Equals("")).Single();
                ViewBag.path = result.path;
            }
            catch
            {

            }
            
            return PartialView();
        }
        [HttpPost]
        public ActionResult Editor(int id)
        {
            file result = db.files.Where<file>(x => x.id_file == id).Single<file>();
            return PartialView(result);
        }
        [HttpPost]
        public int SubmitFile2(string nama, string path)
        {
            try
            {
                file newFile = db.files.Where<file>(x => x.path == path).Single<file>();
                newFile.nama_file = nama;
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        [HttpPost]
        public int SubmitFile1(string path)
        {
            file newFile = new file();
            newFile.nama_file = "";
            newFile.path = path;

            db.files.Add(newFile);
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
        public int EditFile(int id, string nama, string path)
        {
            file editFile = db.files.Where(x => x.id_file == id).Single<file>();
            editFile.nama_file = nama;
            editFile.path = path;

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

        public int Delete(int id)
        {
            try{
                file delFile = db.files.Where(x=>x.id_file == id).Single<file>();
                db.files.Remove(delFile);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        
        public ActionResult GetDownload(int id)
        {
            //ambil filename di database
            file result = db.files.Where(x => x.id_file == id).Single<file>();
            string filename = result.path;
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Upload/FileSharing/" + filename));
                bool exist = System.IO.File.Exists(Server.MapPath("~/Upload/FileSharing/" + filename));
                if (!exist)
                {
                    return null;
                }

                string fileType = getFileType(filename);
                return File(fs, fileType, filename);
            }
            catch (Exception e)
            {
                return null;
                //throw new HttpException(404, "Couldn't find " + filename);
            }
        }
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
                    break;
            };
            return "";
        }
    }
}

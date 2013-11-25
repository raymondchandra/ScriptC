using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Proyek_Informatika.Models;
using System.Data;
using Proyek_Informatika.Controllers.Utilities;
using System.Globalization;

namespace Proyek_Informatika.Controllers.Koordinator
{
    public class SidangKoordinatorController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        private QueryController query = new QueryController();

#region Pengaturan Jadwal
        public ActionResult Jadwal_Navigator()
        {
            return PartialView();
        }
    #region Pengaturan
        public ActionResult Jadwal_Pengaturan()
        {
            var getCurrentFromDb = db.semesters.Where(x => x.isCurrent == 1).Select(y => y.id).SingleOrDefault();
            ViewBag.semester_id = getCurrentFromDb;
            return PartialView();
        }

        public bool SimpanPengaturanJadwal(periode_sidang periode_sidang)
        {
            var temp = db.periode_sidang.Where(x => x.semester_id == periode_sidang.semester_id).ToList();
            if (temp.Count == 0)
            {
                db.periode_sidang.Add(periode_sidang);
            }
            else
            {
                var update = temp.SingleOrDefault();
                update.semester_id = periode_sidang.semester_id;
                update.tipe_sidang = periode_sidang.tipe_sidang;
                update.start_date = periode_sidang.start_date;
                update.end_date = periode_sidang.end_date;
                db.Entry(update).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetStartDate(int semester_id, string tipe)
        {
            DateTime temp = db.periode_sidang.Where(x => x.semester_id == semester_id && x.tipe_sidang == tipe).Select(y => y.start_date).SingleOrDefault();
            if(temp.Year == 1){
                temp = DateTime.Now;
            }
            return temp.Day +"/"+ temp.Month +"/"+temp.Year;
        }

        public string GetEndDate(int semester_id, string tipe)
        {
            DateTime temp = db.periode_sidang.Where(x => x.semester_id == semester_id && x.tipe_sidang == tipe).Select(y => y.end_date).SingleOrDefault();
            if (temp.Year == 1)
            {
                temp = DateTime.Now;
            }
            return temp.Day + "/" + temp.Month + "/" + temp.Year;
        }
    #endregion

    #region Penempatan Mahasiswa
        public ActionResult Jadwal_Penempatan1()
        {
            var getCurrentFromDb = db.semesters.Where(x => x.isCurrent == 1).Select(y=>y.id).SingleOrDefault();
            ViewBag.show = true;
            return PartialView();
        }

        public ActionResult Jadwal_Penempatan2()
        {
            var getCurrentFromDb = db.semesters.Where(x => x.isCurrent == 1).Select(y => y.id).SingleOrDefault();
            ViewBag.show = true;
            return PartialView();
        }

        public ActionResult Jadwal_Penempatan_PopUp()
        {
            return PartialView();
        }

        [GridAction]
        public ActionResult _SelectJadwalSidang(int skripsi)
        {
            var listResult = (from table in db.mahasiswas
                              join table2 in db.skripsis on table.NPM equals table2.NPM_mahasiswa
                              where (table2.jenis == skripsi && table.status == "aktif")
                              select table).ToList();
            List<mahasiswa> temp = new List<mahasiswa>();
            foreach (var item in listResult)
            {
                mahasiswa newMahasiswa = new mahasiswa();
                newMahasiswa.NPM = item.NPM;
                newMahasiswa.nama = item.nama;
                temp.Add(newMahasiswa);
            }
            return View(new GridModel<mahasiswa>
            {
                Data = temp
            });
        }

        public JsonResult ViewDetails(string npm)
        {
            var temp = (from table in db.mahasiswas
                        join table2 in db.skripsis on table.NPM equals table2.NPM_mahasiswa
                        join table3 in db.dosens on table2.NIK_dosen_pembimbing equals table3.NIK
                        join table4 in db.semesters on table2.id_semester_pengambilan equals table4.id
                        where (table.NPM == npm && table4.isCurrent == 1)
                        select new
                        {
                            npm = table.NPM,
                            nama = table.nama,
                            pembimbing = table3.nama
                        }).ToList();
            if (temp.Count() == 1)
            {
                var result = temp.SingleOrDefault();
                var topik = (from table in db.topiks
                             join table2 in db.skripsis on table.id equals table2.id_topik
                             where (table2.NPM_mahasiswa == result.npm)
                             select table.judul).SingleOrDefault();
                return Json(new
                {
                    npm = result.npm,
                    nama = result.nama,
                    pembimbing = result.pembimbing,
                    topik = topik
                });
            }
            else
            {
                //harus nya ga mungkin
                return Json(new
                {
                    npm = "-",
                    nama = "-",
                    pembimbing = "-"
                });
            }
        }

        public JsonResult GetPopUpDetails(string npm)
        {
            var semester = int.Parse(Session["id-semester"].ToString());
            var temp = (from table in db.mahasiswas
                        join table2 in db.skripsis on table.NPM equals table2.NPM_mahasiswa
                        join table3 in db.sidangs on table2.id equals table3.id_skripsi
                        where (table.NPM == npm && table2.id_semester_pengambilan == semester)
                        select table3).ToList();
            if (temp.Count == 0)
            {
                return Json(new
                {
                    success = false
                });
            }
            else
            {
                var get = temp.SingleOrDefault();
                var d = get.tanggal.ToString();
                return Json(new
                {
                    success = true,
                    get.penguji1,
                    get.penguji2,
                    get.ruang,
                    tanggal = d
                });
            }
        }
        public JsonResult ResetPenempatan(string npm)
        {
            //cek udh ada blm
            var temp = (from table in db.mahasiswas
                        join table2 in db.skripsis on table.NPM equals table2.NPM_mahasiswa
                        join table3 in db.sidangs on table2.id equals table3.id_skripsi
                        join table4 in db.semesters on table2.id_semester_pengambilan equals table4.id
                        where (table.NPM == npm && table4.isCurrent == 1)
                        select table3).ToList();
            if (temp.Count == 0)
            {
                return Json(new
                {
                    success = false
                });
            }
            else
            {
                var get = temp.SingleOrDefault();
                db.sidangs.Remove(get);
                db.SaveChanges();

                return Json(new
                {
                    success = true
                });
            }
                
        }
        #region pilih jadwal
        public JsonResult GetDosenList()
        {

            var listResultTemp = db.dosens.Where(x=>x.aktif==1).Select(x => x.nama).ToList();
            return Json(listResultTemp);
        }

        public JsonResult GetTanggalSidangList(string npm, string pembimbing, string penguji)
        {
            var periodeList = (from table in db.periode_sidang
                          select table).ToList();
            if (periodeList.Count != 0)
            {
                var periode = periodeList.SingleOrDefault();
                DateTime start = periode.start_date;
                List<DateTime> dateList = new List<DateTime>();

                while (start.ToString() != periode.end_date.ToString())
                {
                    for (int i = 7; i <= 16; i++)
                    {
                        DateTime temp = start.AddHours(i);
                        dateList.Add(temp);
                    }
                    start = start.AddDays(1);
                }
                for (int i = 7; i <= 16; i++)
                {
                    DateTime temp = start.AddHours(i);
                    dateList.Add(temp);
                }

                string mahasiswaUsername = db.mahasiswas.Where(x => x.NPM == npm).Select(y => y.username).SingleOrDefault();
                var mahasiswaTakKosong = this.GetJadwalTakKosongFromUsername(mahasiswaUsername);
                string pembimbingUsername = db.dosens.Where(x => x.nama == pembimbing).Select(y => y.username).SingleOrDefault();
                var pembimbingTakKosong = this.GetJadwalTakKosongFromUsername(pembimbingUsername);
                string pengujiUsername = db.dosens.Where(x => x.nama == penguji).Select(y => y.username).SingleOrDefault();
                var pengujiTakKosong = this.GetJadwalTakKosongFromUsername(pengujiUsername);
                var jadwal1 = this.GetJadwalNgujiKecualiMahasiswaItu(npm, pembimbingUsername);
                var jadwal2 = this.GetJadwalNgujiKecualiMahasiswaItu(npm, pengujiUsername);
                var summary = dateList.Except(mahasiswaTakKosong).Except(pembimbingTakKosong).Except(pengujiTakKosong).Except(jadwal1).Except(jadwal2);

                List<string> retSummary = new List<string>();
                foreach (var item in summary)
                {
                    retSummary.Add(item.ToString());
                }
                return Json(retSummary);
            }
            else
            {
                //harusnya gamungkin
                return Json(null);
            }
            
        }

        public List<DateTime> GetJadwalNgujiKecualiMahasiswaItu(string npm, string username)
        {
            var id = db.dosens.Where(x => x.username == username).Select(y => y.NIK).SingleOrDefault();

            var semester = int.Parse(Session["id-semester"].ToString());
            var resultList = (from table in db.sidangs
                              join table2 in db.skripsis on table.id_skripsi equals table2.id
                              where ((table.penguji1 == id || table.penguji2 == id || table2.NIK_dosen_pembimbing == id) && table2.id_semester_pengambilan == semester)
                              select new
                              {
                                  table2.NPM_mahasiswa,
                                  table.tanggal
                              }).ToList();

            List<DateTime> dateList = new List<DateTime>();
             foreach (var item in resultList)
            {
               if(item.NPM_mahasiswa !=npm){
                    DateTime startTemp = item.tanggal;
                    DateTime endTemp = item.tanggal.AddHours(2);
                    DateTime start = new DateTime(startTemp.Year, startTemp.Month, startTemp.Day, startTemp.Hour, 0, 0);
                    DateTime end = new DateTime(endTemp.Year, endTemp.Month, endTemp.Day, endTemp.Hour, 0, 0);
                    if (start.ToString() == end.ToString() && start.Hour >= 7 && start.Hour <= 16)
                    {
                        dateList.Add(start);
                    }
                    while (start.CompareTo(end) < 0)
                    {
                        if (start.Hour < 7)
                        {
                            start = new DateTime(start.Year, start.Month, start.Day, 7, 0, 0);
                        }
                        else if (start.Hour >= 7 && start.Hour <= 16)
                        {
                            start = start.AddHours(1);
                            dateList.Add(start);
                        }
                        else if (start.Hour > 16)
                        {
                            DateTime temp = start.AddDays(1);
                            start = new DateTime(temp.Year, temp.Month, temp.Day, 7, 0, 0);
                        }
                    }
               }
            }
            return dateList;
        }

        public List<DateTime> GetJadwalTakKosongFromUsername(string username)
        {
            var getJadwalList = (from table in db.jadwal_tidak_kosong
                                 where (table.username == username)    
                                 select table).ToList();
            List<DateTime> dateList = new List<DateTime>();
            foreach (var item in getJadwalList)
            {
                DateTime startTemp = item.tanggal_mulai;
                DateTime endTemp = item.tanggal_selesai;
                DateTime start = new DateTime(startTemp.Year, startTemp.Month, startTemp.Day, startTemp.Hour, 0, 0);
                DateTime end = new DateTime(endTemp.Year, endTemp.Month, endTemp.Day, endTemp.Hour, 0, 0);
                if (start.ToString() == end.ToString() && start.Hour >= 7 && start.Hour <= 16)
                {
                    dateList.Add(start);
                }
                while (start.ToString() != end.ToString())
                {
                    if (start.Hour < 7)
                    {
                        start = new DateTime(start.Year, start.Month, start.Day, 7, 0, 0);
                    }
                    else if (start.Hour >= 7 && start.Hour <= 16)
                    {
                        start = start.AddHours(1);
                        dateList.Add(start);
                    }
                    else if (start.Hour > 16)
                    {
                        start = new DateTime(start.Year, start.Month, start.Day + 1, 7, 0, 0);
                    }
                }
            }
            return dateList;
        }
        public JsonResult GetRuangSidang()
        {
            var listResult = db.ruangs.Select(x => new{x.id, x.nama_ruang}).ToList();
            return Json(listResult);
        }

        public JsonResult GetRuangKosong(string npm, string waktu)
        {
            var semester = int.Parse(Session["id-semester"].ToString());
            DateTime convert = DateTime.ParseExact(waktu, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            List<ruang> listResult = (from table in db.ruangs
                              join table2 in db.sidangs on table.id equals table2.ruang
                              where (table2.skripsi.id_semester_pengambilan == semester && table2.tanggal == convert && table2.skripsi.NPM_mahasiswa !=npm)
                              select table).ToList();
            List<ruang> allRuang = db.ruangs.ToList();
            List<ruang> ruang = allRuang.Except(listResult).ToList();
            List<object> listRuang = new List<object>();
            foreach (var item in ruang)
            {
                listRuang.Add(new ruang()
                {
                    id = item.id,
                    nama_ruang = item.nama_ruang
                });
            }
            return Json(listRuang);

        }

        public JsonResult GetPenguji2(string npm, string waktu)
        {
            var dosenList = db.dosens.Where(x=>x.aktif == 1).ToList(); //pas waktu itu bisa nguji
            List<object> result = new List<object>();
            foreach (var item in dosenList)
            {
                var pengujiTakKosong = this.GetJadwalTakKosongFromUsername(item.username);
                var pengujiNguji = this.GetJadwalNgujiKecualiMahasiswaItu(npm,item.username);
                var union = pengujiNguji.Union(pengujiTakKosong);
                var can = true;
                foreach (var item2 in union)
                {
                    if(item2.ToString() == waktu){
                        can = false;  
                    }
                }
                if(can){
                    result.Add(new
                    {
                        NIK = item.NIK,
                        nama = item.nama
                    });
                }
            }
            return Json(result);

        }

        public bool SimpanJadwal(string npm, string tanggal, string penguji1, string penguji2, int ruang = 0)
        {

            var skripsi_id = query.GetThisIdFromNPM(npm);
            var checkExist = (from table in db.sidangs
                              where (table.id_skripsi == skripsi_id)
                              select table).ToList();
            if (checkExist.Count == 0)
            {
                sidang newSidang = new sidang();
                newSidang.id_skripsi = skripsi_id;
                newSidang.penguji1 = penguji1;
                newSidang.penguji2 = (penguji2 == "--") ? null: penguji2;
                newSidang.ruang = ruang;
                newSidang.tanggal = DateTime.ParseExact(tanggal, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                db.sidangs.Add(newSidang);
            }
            else
            {
                sidang getSidang = checkExist.SingleOrDefault();
                getSidang.id_skripsi = skripsi_id;
                getSidang.penguji1 = penguji1;
                getSidang.penguji2 = (penguji2=="--")?null:penguji2;
                getSidang.ruang = ruang;
                getSidang.tanggal = DateTime.ParseExact(tanggal, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                
                db.Entry(getSidang).State = EntityState.Modified;
            }
            
            try
            {
                db.SaveChanges();
                return true;
            }catch{
                return false;
            }
        }
        #endregion
    #endregion


#endregion

        #region Lihat Jadwal
        public ActionResult Jadwal_Lihat()
        {
            return PartialView();
        }

        public ActionResult Jadwal_Lihat_Kalender(int periode,string tipe){
            var result = (from table in db.periode_sidang
                          where (table.semester_id == periode && table.tipe_sidang == tipe)
                          select table).ToList();
            if (result.Count != 0)
            {
                periode_sidang getResult = result.SingleOrDefault();
                int[] start = { getResult.start_date.Day, getResult.start_date.Month, getResult.start_date.Year };
                int[] end = { getResult.end_date.Day, getResult.end_date.Month, getResult.end_date.Year };
                ViewBag.start_date = start;
                ViewBag.end_date = end;
                ViewBag.periode = periode;
                ViewBag.tipe = tipe;
                ViewBag.exist = true;
            }
            else
            {
                ViewBag.exist = false;
            }
            
            return PartialView();
        }

        public JsonResult Data(int periode, string tipe)
        {
            int jenis_skripsi = 0;
            if(tipe == "Presentasi"){
                jenis_skripsi = 1;
            }else if(tipe == "Akhir" ){
                jenis_skripsi = 2;
            }
            //events for loading to scheduler
            var items = from table in db.sidangs
                        join table2 in db.skripsis on table.id_skripsi equals table2.id
                        where (table2.id_semester_pengambilan == periode && table2.jenis == jenis_skripsi)
                        select table;
            List<object> listResult = new List<object>();
            foreach (var result in items.ToList())
            {
                string pattern = "yyyy-MM-dd HH:mm:ss";
                string start_date = result.tanggal.ToString(pattern);
                string end_date = result.tanggal.AddHours(2).ToString(pattern);
                var mahasiswa = (from table in db.mahasiswas
                            join table2 in db.skripsis on table.NPM equals table2.NPM_mahasiswa
                            where table2.id == result.id_skripsi
                            select new{table.nama,table.NPM,table2.NIK_dosen_pembimbing}).SingleOrDefault();
                string text = mahasiswa.NPM;
                var pembimbing = db.dosens.Where(x => x.NIK == mahasiswa.NIK_dosen_pembimbing).Select(y => y.inisial).SingleOrDefault();
                var ruang = db.ruangs.Where(x => x.id == result.ruang).Select(y => y.nama_ruang).SingleOrDefault();
                var penguji1 = db.dosens.Where(x => x.NIK == result.penguji1).Select(y => y.inisial).SingleOrDefault();
                var penguji2 = db.dosens.Where(x => x.NIK == result.penguji2).Select(y => y.inisial).SingleOrDefault();
                listResult.Add(new
                {

                    result.id,
                    text,
                    pembimbing,
                    nama = mahasiswa.nama,
                    start_date,
                    end_date,
                    ruang,
                    penguji1,
                    penguji2
                });
            }
            return Json(listResult);

        }




        //public ActionResult Save(FormCollection actionValues)
        //{
        //    String ids = actionValues["ids"];
        //    String action_type = actionValues[ids + "_!nativeeditor_status"];
        //    Int64 source_id = Int64.Parse(actionValues[ids + "_id"]);
        //    Int64 target_id = source_id;


        //    try
        //    {
        //        jadwal_tidak_kosong changedEvent = new jadwal_tidak_kosong();
        //        //changedEvent.id = (Int32)Int64.Parse(actionValues[ids + "_id"]);
        //        changedEvent.text = actionValues[ids + "_text"];
        //        changedEvent.tanggal_mulai = Convert.ToDateTime(actionValues[ids + "_start_date"]);
        //        changedEvent.tanggal_selesai = Convert.ToDateTime(actionValues[ids + "_end_date"]);
        //        changedEvent.description = actionValues[ids + "_description"];

        //        switch (action_type)
        //        {
        //            case "inserted":
        //                changedEvent.username = Session["username"].ToString();
        //                db.jadwal_tidak_kosong.Add(changedEvent);
        //                break;
        //            case "deleted":
        //                changedEvent = db.jadwal_tidak_kosong.SingleOrDefault(ev => ev.id == source_id);
        //                db.jadwal_tidak_kosong.Remove(changedEvent);
        //                break;
        //            default: // "updated"                          
        //                changedEvent = db.jadwal_tidak_kosong.SingleOrDefault(ev => ev.id == source_id);
        //                TryUpdateModel(changedEvent);
        //                break;
        //        }

        //        db.SaveChanges();
        //        target_id = changedEvent.id;
        //    }
        //    catch (Exception a)
        //    {
        //        action_type = "error";
        //    }
        //    return View(new CalendarActionResponseModel(action_type, source_id, target_id));
        //}


#endregion

        #region Pengaturan Sidang
        public ActionResult Sidang_Index()
        {
            ViewBag.username = Session["username"].ToString();
            //var kategori = db.kategori_nilai.Where(x => x.tipe == "general" && x.kategori == "pembimbing" && x.jenis_skripsi_id == 1).ToList();
            //var kategori1 = db.kategori_nilai.Where(x => x.tipe == "general" && x.kategori == "pembimbing" && x.jenis_skripsi_id == 2).ToList();
            //var kategori2 = db.kategori_nilai.Where(x => x.tipe == "general" && x.kategori == "penguji1" && x.jenis_skripsi_id == 2).ToList();
            //var kategori3 = db.kategori_nilai.Where(x => x.tipe == "general" && x.kategori == "penguji2" && x.jenis_skripsi_id == 2).ToList();
            //var kategori4 = db.kategori_nilai.Where(x => x.tipe == "final" && x.kategori == "nilaiAkhir" && x.jenis_skripsi_id == 1).ToList();
            //var kategori5 = db.kategori_nilai.Where(x => x.tipe == "final" && x.kategori == "nilaiAkhir" && x.jenis_skripsi_id == 2).ToList();
            //if(kategori.Count == 0){
            //    kategori_nilai newKategori = new kategori_nilai();
            //    newKategori.tipe = "general";
            //    newKategori.kategori = "pembimbing";
            //    newKategori.jenis_skripsi_id = 1;
            //    newKategori.bobot = 100;
            //    db.kategori_nilai.Add(newKategori);
            //    db.SaveChanges();
            //}
            //if (kategori1.Count == 0)
            //{
            //    kategori_nilai newKategori1 = new kategori_nilai();
            //    newKategori1.tipe = "general";
            //    newKategori1.kategori = "pembimbing";
            //    newKategori1.jenis_skripsi_id = 2;
            //    newKategori1.bobot = 20;
            //    db.kategori_nilai.Add(newKategori1);
            //    db.SaveChanges();
            //}
            //if (kategori2.Count == 0)
            //{
            //    kategori_nilai newKategori2 = new kategori_nilai();
            //    newKategori2.tipe = "general";
            //    newKategori2.kategori = "penguji1";
            //    newKategori2.jenis_skripsi_id = 2;
            //    newKategori2.bobot = 40;
            //    db.kategori_nilai.Add(newKategori2);
            //    db.SaveChanges();
            //}
            //if (kategori3.Count == 0)
            //{
            //    kategori_nilai newKategori3 = new kategori_nilai();
            //    newKategori3.tipe = "general";
            //    newKategori3.kategori = "penguji2";
            //    newKategori3.jenis_skripsi_id = 2;
            //    newKategori3.bobot = 40;
            //    db.kategori_nilai.Add(newKategori3);
            //    db.SaveChanges();
            //}
            //if (kategori4.Count == 0)
            //{
            //    kategori_nilai newKategori4 = new kategori_nilai();
            //    newKategori4.tipe = "final";
            //    newKategori4.kategori = "nilaiAkhir";
            //    newKategori4.jenis_skripsi_id = 1;
            //    newKategori4.bobot = 100;
            //    db.kategori_nilai.Add(newKategori4);
            //    db.SaveChanges();
            //}
            //if (kategori5.Count == 0)
            //{
            //    kategori_nilai newKategori5 = new kategori_nilai();
            //    newKategori5.tipe = "final";
            //    newKategori5.kategori = "nilaiAkhir";
            //    newKategori5.jenis_skripsi_id = 2;
            //    newKategori5.bobot = 100;
            //    db.kategori_nilai.Add(newKategori5);
            //    db.SaveChanges();
            //}
            return PartialView();
        }

        public ActionResult Sidang_Bobot1()
        {
            return PartialView();
        }

        public ActionResult Sidang_Bobot2()
        {
            return PartialView();
        }
        #endregion

        #region grid bobot
        [GridAction]
        public ActionResult _SelectBobot(string tipe, byte jenisSkripsi)
        {
            return bindingTable(tipe, jenisSkripsi);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveBobot(int id, string tipe, byte jenisSkripsi)
        {
            var getKategori = db.kategori_nilai.Where(p => p.id == id).First();
            var total = this.HitungPersentase(getKategori.tipe,2);
            var bobotTemp = getKategori.bobot;
            TryUpdateModel(getKategori);
            total = total + getKategori.bobot - bobotTemp;
            if (total <= 100)
            {
                db.Entry(getKategori).State = EntityState.Modified;
                db.SaveChanges();
            }
            return bindingTable(tipe, jenisSkripsi);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertBobot(string tipe,byte jenisSkripsi)
        {
            kategori_nilai kategori = new kategori_nilai();
            if (TryUpdateModel(kategori))
            {
                kategori.tipe = tipe;
                kategori.jenis_skripsi_id = (byte) jenisSkripsi;
                var total = this.HitungPersentase(kategori.tipe, 2);
                total = total + kategori.bobot;
                if (total <= 100)
                {
                    db.kategori_nilai.Add(kategori);
                    db.SaveChanges();
                }
                
            }
            return bindingTable(tipe,jenisSkripsi);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteBobot(int id, string tipe, byte jenisSkripsi)
        {
            var getKategori = db.kategori_nilai.Where(p => p.id == id).First();
            db.kategori_nilai.Remove(getKategori);
            db.SaveChanges();
            return bindingTable(tipe,jenisSkripsi);
        }

        protected ViewResult bindingTable(string tipe, byte jenisSkripsi)
        {
            var listResult = (from table in db.kategori_nilai
                              where (table.tipe == tipe && jenisSkripsi == table.jenis_skripsi_id)
                              select table).ToList();
            var temp = new List<kategori_nilai>();
            foreach (var item in listResult)
            {
                kategori_nilai nilai = new kategori_nilai();
                nilai.id = item.id;
                nilai.bobot = item.bobot;
                nilai.jenis_skripsi_id = item.jenis_skripsi_id;
                nilai.kategori = item.kategori;
                nilai.tipe = item.tipe;
                nilai.urutan = item.urutan;
                temp.Add(nilai);
            }
            return View(new GridModel<kategori_nilai>
            {
                Data = temp
            });
        }
    #endregion

    #region ruang
        [GridAction]
        public ActionResult _SelectRuang()
        {
            return bindingTableRuang();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveRuang(int id)
        {
            var getRuang = db.ruangs.Where(p => p.id == id).First();
            TryUpdateModel(getRuang);
            db.Entry(getRuang).State = EntityState.Modified;
            db.SaveChanges();
            return bindingTableRuang();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertRuang()
        {
            ruang ruang = new ruang();
            if (TryUpdateModel(ruang))
            {
                db.ruangs.Add(ruang);
                db.SaveChanges();
            }
            return bindingTableRuang();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteRuang(int id)
        {
            var getRuang = db.ruangs.Where(p => p.id == id).First();
            db.ruangs.Remove(getRuang);
            db.SaveChanges();
            return bindingTableRuang();
        }

        protected ViewResult bindingTableRuang()
        {
            var listResultTemp = (from table in db.ruangs
                              select table).ToList();
            List<ruang> listResult = new List<ruang>();
            foreach (var item in listResultTemp)
            {
                ruang newRuang = new ruang();
                newRuang.id = item.id;
                newRuang.nama_ruang = item.nama_ruang;
                listResult.Add(newRuang);
            }
            return View(new GridModel<ruang>
            {
                Data = listResult
            });
        }
    #endregion

        public int GetBobot(string tipe, byte jenis_skripsi_id)
        {
            var listResult = (from table in db.kategori_nilai
                              where (table.tipe == "general" && table.kategori == tipe && table.jenis_skripsi_id == jenis_skripsi_id)
                              select table).ToList();
            if (listResult.Count != 0)
            {
                var result = listResult.First();
                return result.bobot;
            }
            else
            {
                return 0;
            }
        }

        public int HitungPersentase(string pemberiNilai,int skripsi)
        {
            var listResult = (from table in db.kategori_nilai
                              where (table.tipe == pemberiNilai && table.jenis_skripsi_id == skripsi)
                              select table).ToList();
            if (listResult.Count != 0)
            {
                
                return listResult.Sum(x=>x.bobot);
            }
            else
            {
                return 0;
            }
        }

       
        public bool SimpanBobotGeneral(kategori_nilai newRow)
        {
            var find = db.kategori_nilai.Where(x=>x.tipe=="general" && x.kategori == newRow.kategori).ToList();
            
            if (find.Count == 0)
            {
                db.kategori_nilai.Add(newRow);
            }
            else
            {
                var getKategori = find.First();
                var selisih = newRow.bobot - getKategori.bobot;
                var total = HitungTotalBobotGeneralTemp(getKategori.jenis_skripsi_id, selisih);
                if(total > 100){
                    return false;
                }
                getKategori.bobot = newRow.bobot;
                db.Entry(getKategori).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public int HitungTotalBobotGeneralTemp(int jenis_skripsi,int selisih)
        {
            var list = db.kategori_nilai.Where(x=>x.tipe=="general" && x.jenis_skripsi_id == jenis_skripsi).ToList();
            int a = list.Sum(x => x.bobot);
            return a + selisih;
        }
        public bool Mulai_Pengumpulan(bool mulai,int semester_id, string tipe)
        {
            var getPeriodeSidang = db.periode_sidang.Where(x => x.semester_id == semester_id && x.tipe_sidang == tipe).ToList();
            if (getPeriodeSidang.Count != 0)
            {
                var get = getPeriodeSidang.SingleOrDefault();
                if (mulai)
                {
                    get.status = "open";
                }
                else
                {
                    get.status = "close";
                }
                try
                {
                    db.Entry(get).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }   

		public JsonResult GetBatasPeriode(string npm, string date)
        {
            var semester = int.Parse(Session["id-semester"].ToString());
            var tipe_sidang = db.skripsis.Where(x=>x.NPM_mahasiswa == npm && semester == x.id_semester_pengambilan).Select(y=>y.jenis).SingleOrDefault();
            string tipe;
            if(tipe_sidang == 1){
              tipe  = "Presentasi";
            }else{
               tipe = "Akhir";
            }
            var periode = db.periode_sidang.Where(x => x.semester_id == semester && x.tipe_sidang == tipe).SingleOrDefault();
            if (date == "min")
            {

                return Json(new
                {
                    year = periode.start_date.Year,
                    month = periode.start_date.Month,
                    date = periode.start_date.Day,
                    hour = periode.start_date.Hour,
                    minute = periode.start_date.Minute
                });
            }
            else
            {
                return Json(new
                {
                    year = periode.end_date.Year,
                    month = periode.end_date.Month,
                    date = periode.end_date.Day,
                    hour = periode.end_date.Hour,
                    minute = periode.end_date.Minute
                });
            }
            
        }
    }
}

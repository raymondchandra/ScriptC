using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;
using System.Data;

namespace Proyek_Informatika.Controllers.Dosen
{
    public class SidangDosenController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();

        //
        // GET: /Sidang/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Penilaian()
        {
            return PartialView();
        }

        public ActionResult Pengumpulan_Index()
        {
            return PartialView();
        }

        public ActionResult Sidang_Index(string role, int skripsi_id)
        {
            ViewBag.role = role;
            ViewBag.skripsi_id = skripsi_id;
            var resultList = (from table in db.sidangs
                              join table2 in db.skripsis on table.id_skripsi equals table2.id
                              where (table.id_skripsi == skripsi_id)
                              select new
                              {
                                  table2.NPM_mahasiswa,
                                  table2.NIK_dosen_pembimbing,
                                  table.penguji1,
                                  table.penguji2
                              }).SingleOrDefault();
            var a = db.mahasiswas.Where(x => x.NPM == resultList.NPM_mahasiswa).Select(y => y.nama).SingleOrDefault();
            ViewBag.npm = resultList.NPM_mahasiswa;
            ViewBag.nama = a;
            ViewBag.topik = (from table in db.topiks
                             join table2 in db.skripsis on table.id equals table2.id_topik
                             where (table2.id_topik == skripsi_id)
                             select table.judul).SingleOrDefault();
            ViewBag.pembimbing = db.dosens.Where(x => x.NIK == resultList.NIK_dosen_pembimbing).Select(y => y.nama).SingleOrDefault();
            ViewBag.penguji1 = db.dosens.Where(x => x.NIK == resultList.penguji1).Select(y => y.nama).SingleOrDefault();

            if (resultList.penguji2 == null)
            {
                ViewBag.penguji2 = "-";
            }
            else
            {
                ViewBag.penguji2 = db.dosens.Where(x => x.NIK == resultList.penguji2).Select(y => y.nama).SingleOrDefault();
            }

            return PartialView();
        }

        public ActionResult Sidang(string role, int skripsi_id)
        {
            ViewBag.skripsi_id = skripsi_id;
            ViewBag.role = role;
            var jenis_skripsi_id = db.skripsis.Where(x => x.id == skripsi_id).Select(y => y.jenis).SingleOrDefault();
            ViewBag.jenis_skripsi = jenis_skripsi_id;
            //string roleMod = role;
            //if(roleMod != "pembimbing"){
            //    roleMod = role.Substring(0,role.Length-1);
            //}
            var bobotTemp = this.GetBobotKategori(role, jenis_skripsi_id);
            List<Tuple<int,string,int,int>> bobotList = new List<Tuple<int,string,int,int>>();
            foreach (var item in bobotTemp)
	        {
                var temp = db.nilais.Where(x => x.id_skripsi == skripsi_id && x.kategori == item.Item4).Select(y => y.angka).SingleOrDefault();
                int angka = (int) (temp * 100 / item.Item3);
                bobotList.Add(new Tuple<int,string,int,int>(item.Item1,item.Item2,item.Item3,angka));
                
	        }
            
            ViewData["bobot"] = bobotList;
            return PartialView();
        }

        public ActionResult Sidang_Koordinator(int skripsi_id)
        {
            ViewBag.skripsi_id = skripsi_id;
            var jenis_skripsi_id = db.skripsis.Where(x => x.id == skripsi_id).Select(y => y.jenis).SingleOrDefault();
            ViewBag.jenis_skripsi_id = jenis_skripsi_id;
            var jmlhNilai = db.nilais.Where(x=>x.id_skripsi == skripsi_id && x.kategori_nilai.tipe == "general" && x.submitted == 1).ToList();
            if (jenis_skripsi_id == 1 && jmlhNilai.Count == 1)
            {
                ViewBag.showhide = true;
            }
            else if (jenis_skripsi_id == 2 && jmlhNilai.Count == 3)
            {
                ViewBag.showhide = true;
            }
            else
            {
                ViewBag.showhide = false;
            }
            
            ViewData["bobot2"] = this.GetBobotGeneral(skripsi_id,jenis_skripsi_id);
            return PartialView();
        }

        [GridAction]
        public ActionResult _SelectPenilaianSidang()
        {
            var username = Session["username"].ToString();
            var id = db.dosens.Where(x => x.username == username).Select(y => y.NIK).SingleOrDefault();
            var semester = int.Parse(Session["id-semester"].ToString());
            var resultList = (from table in db.sidangs
                              join table2 in db.skripsis on table.id_skripsi equals table2.id
                              where ((table.penguji1 == id || table.penguji2 == id || table2.NIK_dosen_pembimbing == id) && table.akses !=2 && table2.id_semester_pengambilan ==semester)
                              select new{
                                table2.NPM_mahasiswa,
                                table2.NIK_dosen_pembimbing,
                                table.penguji1,
                                table.penguji2,
                                table.ruang,
                                table.tanggal
                              }).ToList();

            List<PenilaianSidangContainer> temp = new List<PenilaianSidangContainer>();
            foreach (var item in resultList)
            {
                var nama = db.mahasiswas.Where(x=>x.NPM == item.NPM_mahasiswa).Select(y=>y.nama).SingleOrDefault();
                var pembimbing = db.dosens.Where(x=>x.NIK == item.NIK_dosen_pembimbing).Select(y=>y.inisial).SingleOrDefault();
                var penguji1 = db.dosens.Where(x=>x.NIK == item.penguji1).Select(y=>y.inisial).SingleOrDefault();
                var penguji2 = db.dosens.Where(x=>x.NIK == item.penguji2).Select(y=>y.inisial).SingleOrDefault();
                var ruang = db.ruangs.Where(x=>x.id == item.ruang).Select(y=>y.nama_ruang).SingleOrDefault();
                PenilaianSidangContainer newRow = new PenilaianSidangContainer();
                newRow.nama = nama;
                newRow.pembimbing = pembimbing;
                newRow.NPM = item.NPM_mahasiswa;
                newRow.penguji1 = penguji1;
                newRow.penguji2 = penguji2;
                newRow.ruang = ruang;
                var d = item.tanggal;
                newRow.tanggal = d.Day+"/"+d.Month+"/"+d.Year+" "+d.Hour+":"+d.Minute;
                temp.Add(newRow);
            }
            return View(new GridModel<PenilaianSidangContainer>
            {
                Data = temp
            });
        }

        public JsonResult AksesSidang(string id)
        {

            int idSemester = Int32.Parse(Session["id-semester"].ToString());
            var tempResult = db.skripsis.Where(x => x.NPM_mahasiswa == id && x.id_semester_pengambilan == idSemester).Select(y => y.id).SingleOrDefault();
            return Json(new { 
                id = tempResult
            });
        }

        public string GetRole(int skripsi_id)
        {
            var username = Session["username"].ToString();
            var nik = db.dosens.Where(x => x.username == username).Select(y => y.NIK).SingleOrDefault();
            var result = db.sidangs.Where(x => x.id_skripsi == skripsi_id).SingleOrDefault();
            if (result.penguji1 == nik)
            {
                return "penguji1";
            }
            else if (result.penguji2 == nik)
            {
                return "penguji2";
            }
            else
            {
                return "pembimbing";
            }
        }

        public int CekSidangAkses(int skripsi_id, string role)
        {
            string tipe = role;
            
            var jenis_skripsi = db.skripsis.Where(x=>x.id == skripsi_id).Select(y=>y.jenis).SingleOrDefault();
            var bobot = db.kategori_nilai.Where(x => x.jenis_skripsi_id == jenis_skripsi && x.tipe == tipe).ToList();
            var akses = db.sidangs.Where(x => x.id_skripsi == skripsi_id).Select(y => y.akses).SingleOrDefault();
            if(role == "penguji1" && jenis_skripsi == 1){
                return 1;
            }
            else if (bobot.Count == 0 && jenis_skripsi == 2)
            {
                return 3; //ga ad penilaian
            }
            else if (akses == 1)
            {
                return 1;
            }
            else if(akses == 0 && role == "penguji1")
            {
                return 1;
            }else {
                return 2; //ga ad akses
            }
        }

        public int GetSidangStatus(int skripsi_id)
        {
            var akses = db.sidangs.Where(x => x.id_skripsi == skripsi_id).Select(y=>y.akses).SingleOrDefault();
            return akses;
        }

        public bool MulaiSidang(int skripsi_id)
        {
            var akses = db.sidangs.Where(x => x.id_skripsi == skripsi_id).SingleOrDefault();
            akses.akses = 1;
            db.Entry(akses).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public double GetNilaiSkripsi1(int skripsi_id)
        {
            
            var kategori = db.kategori_nilai.Where(x=>x.tipe == "general" && x.jenis_skripsi_id == 1 && x.kategori == "presentasi").Select(y=>y.id).SingleOrDefault();
            var temp = db.nilais.Where(x => x.id_skripsi == skripsi_id && x.kategori == kategori).Select(y => y.angka).SingleOrDefault();
            return temp;
        }

        public List<Tuple<int, string, int, int>> GetBobotKategori(string role, int jenis_skripsi_id)
        {
            var listTemp = (from table in db.kategori_nilai
                            where (table.tipe == role && table.jenis_skripsi_id == jenis_skripsi_id)
                            orderby table.urutan
                            select table).ToList();
            List<Tuple<int, string, int, int>> listResult = new List<Tuple<int, string, int, int>>();
            foreach (var item in listTemp)
            {
                if (item.kategori != "general")
                {
                    int urut = (int)item.urutan;
                    listResult.Add(new Tuple<int, string, int,int>(urut, item.kategori, item.bobot,item.id));
                } 
                
                
            }
            return listResult;
   
        }

        public List<Tuple<int, string, int, double>> GetBobotGeneral(int skripsi_id, int jenis_skripsi_id)
        {
            var listTemp = (from table in db.kategori_nilai
                            join table2 in db.nilais on table.id equals table2.kategori
                            where (table.tipe == "general" && table.jenis_skripsi_id == jenis_skripsi_id && skripsi_id == table2.id_skripsi)
                            select new { 
                                table.kategori,table.bobot,table2.angka
                            }).ToList();
            List<Tuple<int, string, int, double>> listResult = new List<Tuple<int, string, int, double>>();
            if (jenis_skripsi_id == 1)
            {
                var a = listTemp.Where(x => x.kategori == "presentasi").SingleOrDefault();
                if (a != null)
                {
                    listResult.Add(new Tuple<int, string, int, double>(1, a.kategori, a.bobot, a.angka));

                }
            }
            else if (jenis_skripsi_id == 2)
            {
                var a = listTemp.Where(x => x.kategori == "pembimbing").SingleOrDefault();
                var b = listTemp.Where(x => x.kategori == "penguji1").SingleOrDefault();
                var c = listTemp.Where(x => x.kategori == "penguji2").SingleOrDefault();
                if (a != null)
                {
                    listResult.Add(new Tuple<int, string, int, double>(1, a.kategori, a.bobot, a.angka));

                }
                if (b != null)
                {
                    listResult.Add(new Tuple<int, string, int, double>(2, b.kategori, b.bobot, b.angka));
                }
                if (c != null)
                {
                    listResult.Add(new Tuple<int, string, int, double>(3, c.kategori, c.bobot, c.angka));
                }
            }
            return listResult;
        }

        public bool SimpanNilai(int urutan, float nilai, int skripsi_id,string tipe)
        {
            var jenis_skripsi = db.skripsis.Where(x=>x.id == skripsi_id).Select(y=>y.jenis).SingleOrDefault();
            var kategori = db.kategori_nilai.Where(x=>x.jenis_skripsi_id == jenis_skripsi && x.urutan == urutan && x.tipe == tipe).Select(y=>y.id).SingleOrDefault();
            var cekNilai = (from table in db.nilais
                            where (table.kategori == kategori && table.id_skripsi == skripsi_id)
                            select table).ToList();
            if(cekNilai.Count == 0){
                nilai newNilai= new nilai();
                newNilai.angka = nilai;
                newNilai.kategori = kategori;
                newNilai.id_skripsi = skripsi_id;
                var username = Session["username"].ToString();
                newNilai.NIK_pengisi = db.dosens.Where(x=>x.username == username).Select(y=>y.NIK).SingleOrDefault();
                db.nilais.Add(newNilai);
            }else{
                nilai newNilai = cekNilai.SingleOrDefault();
                newNilai.angka = nilai;
                db.Entry(newNilai).State = EntityState.Modified;
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


        public bool SimpanNilaiSkripsi1( float nilai, int skripsi_id)
        {
            var jenis_skripsi = db.skripsis.Where(x => x.id == skripsi_id).Select(y => y.jenis).SingleOrDefault();
            var kategori = db.kategori_nilai.Where(x => x.jenis_skripsi_id == jenis_skripsi && x.tipe=="general" && x.kategori =="presentasi").Select(y => y.id).SingleOrDefault();
            var cekNilai = (from table in db.nilais
                            where (table.kategori == kategori && table.id_skripsi == skripsi_id)
                            select table).ToList();
            if (cekNilai.Count == 0)
            {
                nilai newNilai = new nilai();
                newNilai.angka = nilai;
                newNilai.kategori = kategori;
                newNilai.id_skripsi = skripsi_id;
                var username = Session["username"].ToString();
                newNilai.NIK_pengisi = db.dosens.Where(x => x.username == username).Select(y => y.NIK).SingleOrDefault();
                db.nilais.Add(newNilai);
            }
            else
            {
                nilai newNilai = cekNilai.SingleOrDefault();
                newNilai.angka = nilai;
                db.Entry(newNilai).State = EntityState.Modified;
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

        public bool SimpanNilaiTotal(int skripsi_id, string kategori)
        {
            double nilai = this.hitungTotal(skripsi_id,kategori);
            var jenis_skripsi = db.skripsis.Where(x => x.id == skripsi_id).Select(y => y.jenis).SingleOrDefault();
            var kategoriID = db.kategori_nilai.Where(x => x.jenis_skripsi_id == jenis_skripsi && x.tipe == "general" && x.kategori == kategori).Select(y => y.id).SingleOrDefault();
            var cekNilai = (from table in db.nilais
                            where (table.kategori == kategoriID && table.id_skripsi == skripsi_id)
                            select table).ToList();
            if (cekNilai.Count == 0)
            {
                nilai newNilai = new nilai();
                newNilai.angka = nilai;
                newNilai.kategori = kategoriID;
                newNilai.id_skripsi = skripsi_id;
                var username = Session["username"].ToString();
                newNilai.NIK_pengisi = db.dosens.Where(x => x.username == username).Select(y => y.NIK).SingleOrDefault();
                db.nilais.Add(newNilai);
            }
            else
            {
                nilai newNilai = cekNilai.SingleOrDefault();
                newNilai.angka = nilai;
                db.Entry(newNilai).State = EntityState.Modified;
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

        public bool SubmitAll(int skripsi_id)
        {
            var username = Session["username"].ToString();
            var nik = db.dosens.Where(x => x.username == username).Select(y => y.NIK).SingleOrDefault();
            var temp = (from table in db.nilais
                       where (table.NIK_pengisi == nik && table.id_skripsi == skripsi_id)
                       select table).ToList();
            try
            {
                foreach (var item in temp)
                {
                    item.submitted = 1;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            catch {
                return false;
            }
        }

        public bool AkhiriSidang(int skripsi_id, double nilai_akhir)
        {
            var username = Session["username"].ToString();
            var nik = db.dosens.Where(x => x.username == username).Select(y => y.NIK).SingleOrDefault();
            var jenis_skripsi_id = db.skripsis.Where(x=>x.id == skripsi_id).Select(y=>y.jenis).SingleOrDefault();
            var kategori = db.kategori_nilai.Where(x=>x.tipe == "final" && x.jenis_skripsi_id == jenis_skripsi_id).Select(y=>y.id).SingleOrDefault();
            var temp = db.nilais.Where(x=>x.id_skripsi == skripsi_id && x.kategori == kategori).ToList();
            if (temp.Count == 0)
            {
                nilai akhir = new nilai();
                akhir.angka = nilai_akhir;
                akhir.id_skripsi = skripsi_id;
                akhir.NIK_pengisi = nik;
                akhir.submitted = 1;
                akhir.kategori = kategori;
                db.nilais.Add(akhir);
            }else{
                nilai akhir = temp.SingleOrDefault();
                akhir.angka = nilai_akhir;
                akhir.id_skripsi = skripsi_id;
                akhir.NIK_pengisi = nik;
                akhir.submitted = 1;
                akhir.kategori = kategori;
                db.Entry(akhir).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                var getSidang = db.sidangs.Where(x => x.id_skripsi == skripsi_id).SingleOrDefault();
                getSidang.akses = 2;
                db.Entry(getSidang).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public double hitungTotal(int skripsi_id, string kategori)
        {
            var nilai = db.nilais.Where(x => x.kategori_nilai.tipe == kategori && x.id_skripsi == skripsi_id).Sum(x=>x.angka);
            return nilai;
        }
    }
}

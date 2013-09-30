using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class MahasiswaJadwalPemesanan
    {
        public int id { get; set; }
        public string status { get; set; }
        public string tanggalPesan { get; set; }
        public string waktu { get; set; }
        public string namaDosen { get; set; }
    }
    public class MahasiswaKartuBimbingan
    {
        public int id { get; set; }
        public string tanggal { get; set; }
        public string bahasan { get; set; }
        public string namaDosen { get; set; }
    }
    public class MahasiswaPengumpulanFile
    {
        public int id{get; set;}
        public string dokumen { get; set; }
        public string namaFile { get; set; }
        public string deskripsi { get; set; }
        public string waktuKumpul { get; set; }
    }

}
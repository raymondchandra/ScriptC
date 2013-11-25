using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    //public class DosenJadwalTerpesan
    //{
    //    public int id { get; set; }
    //    public string tanggalPesan { get; set; }
    //    public string waktu { get; set; }
    //    public string npm { get; set; }
    //    public string nama { get; set; }
    //    public string pesan { get; set; }
    //    public string status { get; set; }
    //}
    //public class DosenKartuBimbingan
    //{
    //    public int id { get; set; }
    //    public string tanggal { get; set; }
    //    public string bahasan { get; set; }
    //    public string detil { get; set; }

    //}
    public class DosenMuridBimbinganContainer
    {
        public int id { get; set; }
        public string npm { get; set; }
        public string namaMahasiswa { get; set; }
        public string judul { get; set; }
    }
}
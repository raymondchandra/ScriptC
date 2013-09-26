using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class DosenJadwalTerpesan
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Pemesanan")]
        public string TanggalPesan { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Waktu Pemesanan")]
        public string Waktu { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "NPM")]
        public string NPM{ get; set; }
    }
    public class DosenKartuBimbingan
    {
        [DataType(DataType.Date)]
        [Display(Name="Tanggal")]
        public string tanggal { get; set;}

        [DataType(DataType.Text)]
        [Display(Name = "Bahasan")]
        public string bahasan{ get; set; }
    }
    public class DosenPengumpulanFile
    {
        [DataType(DataType.Text)]
        [Display(Name = "File")]
        public string dokumen {get; set;}

        [DataType(DataType.DateTime)]
        [Display(Name = "Waktu Pengumpulan")]
        public string waktuKumpul { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Batas Waktu")]
        public string batasKumpul { get; set; }
    }

}
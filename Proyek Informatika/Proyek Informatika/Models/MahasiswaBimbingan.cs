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
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Pemesanan")]
        public string tanggalPesan { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Waktu Pemesanan")]
        public string waktu { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nama Dosen")]
        public string namaDosen { get; set; }
    }
    public class MahasiswaKartuBimbingan
    {
        [DataType(DataType.Date)]
        [Display(Name = "Tanggal")]
        public string tanggal { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Bahasan")]
        public string bahasan { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nama Dosen")]
        public string namaDosen { get; set; }
    }
    public class MahasiswaPengumpulanFile
    {
        [DataType(DataType.Text)]
        [Display(Name = "File")]
        public string dokumen { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Waktu Pengumpulan")]
        public string waktuKumpul { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Batas Waktu")]
        public string batasKumpul { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Unggah")]
        public string unggah { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class KoordinatorBimbingan
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Periode")]
        public string periode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "NIK Dosen")]
        public string nikDosen { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nama Dosen")]
        public string namaDosen { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "NPM Mahasiswa")]
        public string npmMahasiswa { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nama Mahasiswa")]
        public string namaMahasiswa { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Judul Skripsi")]
        public string judul{ get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Tipe")]
        public string tipe { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Jumlah Bimbingan")]
        public string jumlahBimbingan { get; set; }
    }
    public class KoordinatorKartuBimbingan
    {
        [DataType(DataType.Date)]
        [Display(Name = "Tanggal")]
        public string tanggal { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Bahasan")]
        public string bahasan { get; set; }
    }
    public class KoordinatorPengumpulanFile
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
    }

}
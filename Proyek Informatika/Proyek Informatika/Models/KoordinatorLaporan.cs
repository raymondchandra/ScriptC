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
        public int id { get; set; }
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

        public int jumlahBimbingan { get; set; }
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
        public int id { get; set; }
        public string dokumen { get; set; }
        public string waktuKumpul { get; set; }
        public string npmMahasiswa { get; set; }
        public string namaMahasiswa { get; set; }
        public string judul { get; set; }
    }

    public class KoordinatorPenilaian
    {
        public int id { get; set; }
        public string npm { get; set; }
        public string nama { get; set; }
        public string judul { get; set; }
        public int nilaiAkhir { get; set; }
    }

    public class KoordinatorPenilaianSkripsi2
    {
        public int id { get; set; }
        public string penilai { get; set; }
        public int komponen1 { get; set; }
        public int komponen2 { get; set; }
        public int komponen3 { get; set; }
        public int nilai1 { get; set; }
        public int nilai2 { get; set; }
        public int nilai3 { get; set; }
    }

}
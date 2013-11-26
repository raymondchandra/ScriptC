using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;


namespace Proyek_Informatika.Models
{
    //test

        public class DosenMuridBimbinganContainer
        {
            public int id { get; set; }
            public string npm { get; set; }
            public string namaMahasiswa { get; set; }
            public string judul { get; set; }
        }
        public class KoordinatorListMahasiswa
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
            public string judul { get; set; }

            public int pengambilanke { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Tipe")]
            public string tipe { get; set; }

            public int jumlahBimbingan { get; set; }
        }
        public class KoordinatorPengumpulanContainer
        {
            public int id { get; set; }
            public string dokumen { get; set; }
            public DateTime waktuKumpul { get; set; }
            public string npmMahasiswa { get; set; }
            public string namaMahasiswa { get; set; }
            public string pembimbing { get; set; }
            public string nikDosen { get; set; }
            public string judul { get; set; }
            public string deskripsi { get; set; }
            public string skripsi { get; set; }
        }
        public class EnumSemesterSkripsiContainer
        {
            public IEnumerable<jenis_skripsi> jenis_skripsi;
            public IEnumerable<semester> jenis_semester;
            public int selected_value;
        }
        public class EnumBimbingan
        {
            public IEnumerable<DosenMuridBimbinganContainer> murid;
        }

        public class EnumTest
        {
            string a;
        }

        public class DosenMuridNilaiContainer
        {
            public int id { get; set; }
            public string npm { get; set; }
            public string namaMahasiswa { get; set; }
            public string judul { get; set; }
            public string status { get; set; }
        }
        public class KoordinatorNilaiMahasiswa
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
            public string judul { get; set; }

            public int pengambilanke { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Tipe")]
            public string tipe { get; set; }

            public double nilai { get; set; }

            public string status { get; set; }
        }
}

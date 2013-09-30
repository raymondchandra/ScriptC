using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class SejarahMahasiswa
    {
        [Display(Name = "No.")]
        public int no { get; set; }

        [Display(Name = "Nama mata Kuliah")]
        public string MataKuliah { get; set; }

        [Display(Name = "Tahun")]
        public int Tahun { get; set; }

         [Display(Name = "Semester")]
        public string Semester { get; set; }

        [Display(Name = "Topik")]
        public string Topik { get; set; }

        [Display(Name = "Nama Pembimbing")]
        public string Pembimbing { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }
    }
}

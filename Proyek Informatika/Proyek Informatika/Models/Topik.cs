using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class Topik
    {
        [Display(Name = "Kode Topik")]
        public int id { get; set; }

        [Display(Name = "Topik")]
        public string Nama { get; set; }

        [Display(Name = "Deskripsi Topik")]
        public string Deskripsi { get; set; }

        [Display(Name = "Nama Pembimbing")]
        public string Pembimbing { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }
    }
}

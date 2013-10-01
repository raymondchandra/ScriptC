using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class PenilaianSidang
    {
        [Display(Name = "NPM")]
        public int id { get; set; }

        [Display(Name = "Nama Mahasiswa")]
        public string mahasiswa { get; set; }

        [Display(Name = "Pembimbing")]
        public string pembimbing { get; set; }

        [Display(Name = "Penguji 1")]
        public string penguji1 { get; set; }
        
        [Display(Name = "Penguji 2")]
        public string penguji2 { get; set; }
    }
}

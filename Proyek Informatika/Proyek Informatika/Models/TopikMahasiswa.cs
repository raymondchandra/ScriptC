using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class TopikMahasiswa
    {
        [Display(Name = "NPM")]
        public int NPM { get; set; }

        [Display(Name = "Nama Mahasiswa")]
        public string Nama { get; set; }

        [Display(Name = "Topik")]
        public string Topik { get; set; }       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    
    public class DosenPengumpulanFile
    {
        public int id { get; set; }
        public int npm { get; set; }
        public string nama { get; set; }
        public string judul { get; set; }
        public string dokumen { get; set; }
        public string waktuKumpul { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class BiodataContainer
    {        
        public string foto { get; set; }
        public string NIK { get; set; }
        public string nama { get; set; }
        public string email { get; set; }
        public byte jenis_skripsi { get; set; }
        public string judul { get; set; }
    }
}

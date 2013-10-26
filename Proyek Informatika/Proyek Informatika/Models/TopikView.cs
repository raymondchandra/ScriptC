using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class TopikView
    {
        public int id { get; set; }
        public string judul { get; set; }
        public string deskripsi { get; set; }
        public string keterangan { get; set; }
        public string pembimbing { get; set; }
    
    }
}

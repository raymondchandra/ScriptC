using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class TopikContainer
    {
        public int id { get; set; }
        
        [Display(Name = "Topik")]
        public string judul { get; set; }
        public string deskripsi { get; set; }
        public string keterangan { get; set; }
        public string pembimbing { get; set; }
    
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class PenempatanContainer
    {
        public int id { get; set; }
        public string npm { get; set; }
        public string nama { get; set; }
        public string ruang { get; set; }
        public int pembimbing { get; set; }
        public int penguji1 { get; set; }
        public int penguji2 { get; set; }
        public DateTime tanggal { get; set; }
    }
}
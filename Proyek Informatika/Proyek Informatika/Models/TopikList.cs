using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class TopikList
    {
        public int id { get; set; }
        public string Nama { get; set; }
        public string Deskripsi { get; set; }
        public string Pembimbing { get; set; }
        public string Keterangan { get; set; }
    }
}

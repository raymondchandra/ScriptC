using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class ListMahasiswa
    {
        public int id { get; set; }
        public string npm { get; set; }
        public string namaMahasiswa { get; set; }
        public string judul { get; set; }
        public int tipeSkripsi { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class PengumumanContainer
    {
        public int id { get; set; }
        public byte target { get; set; }
        public string judul { get; set; }
        public string isi { get; set; }
        public string pembuat { get; set; }
        public DateTime tanggal { get; set; }
    }
}

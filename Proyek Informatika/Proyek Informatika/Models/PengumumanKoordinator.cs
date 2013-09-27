using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class PengumumanKoordinator
    {
        [Display(Name = "Kode Pengumuman")]
        public int id { get; set; }

        [Display(Name = "Judul")]
        public string Judul { get; set; }

        [Display(Name = "Isi")]
        public string Isi { get; set; }

        [Display(Name = "Tanggal")]
        public string Tanggal { get; set; }

        [Display(Name = "Penulis")]
        public string Penulis { get; set; }

        [Display(Name = "Target")]
        public string Target { get; set; }
    }
}

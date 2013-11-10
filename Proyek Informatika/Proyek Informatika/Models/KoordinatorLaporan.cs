using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{


    public class KoordinatorPenilaian
    {
        public int id { get; set; }
        public string npm { get; set; }
        public string nama { get; set; }
        public string judul { get; set; }
        public int nilaiAkhir { get; set; }
    }

    public class KoordinatorPenilaianSkripsi2
    {
        public int id { get; set; }
        public string penilai { get; set; }
        public int komponen1 { get; set; }
        public int komponen2 { get; set; }
        public int komponen3 { get; set; }
        public int nilai1 { get; set; }
        public int nilai2 { get; set; }
        public int nilai3 { get; set; }
    }

}
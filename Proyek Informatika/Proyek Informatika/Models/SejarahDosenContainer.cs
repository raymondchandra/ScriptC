﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class SejarahDosenContainer
    {
        public int id { get; set; }

        [Display(Name = "Periode")]
        public string periode { get; set; }
        [Display(Name = "Mata Kuliah")]
        public string jenis { get; set; }
        [Display(Name = "Topik")]
        public string topik { get; set; }
        [Display(Name = "NPM")]
        public string npm { get; set; }
        [Display(Name = "Mahasiswa")]
        public string mahasiswa { get; set; }
        [Display(Name = "Nilai Akhir")]
        public string nilai { get; set; }

    }
}

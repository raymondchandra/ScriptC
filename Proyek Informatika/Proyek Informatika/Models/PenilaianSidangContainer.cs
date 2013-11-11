using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{

    public class PenilaianSidangContainer
    {
        public string NPM { get; set; }
        public string nama { get; set; }
        public string pembimbing { get; set; }
        public string penguji1 { get; set; }
        public string penguji2 { get; set; }
        public string ruang { get; set; }
        public string tanggal { get; set; }
    }
}

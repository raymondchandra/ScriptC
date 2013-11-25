using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class DosenContainer
    {
        public string NIK { get; set; }
        public string nama { get; set; }
        public string inisial { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
        public string aktif { get; set; }
    }
}

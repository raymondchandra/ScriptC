using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class GradeCategory
    {
        public int id { get; set; }
        public string kategori { get; set; }
        public string tipe { get; set; }
        public string bobot { get; set; }
    }
}

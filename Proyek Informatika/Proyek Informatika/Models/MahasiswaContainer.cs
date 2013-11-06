using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{
    public class MahasiswaContainer
    {
        [Display(Name = "NPM Mahasiswa")]
        public string NPM { get; set; }
        public string namaMahasiswa { get; set; }
        public string emailMahasiswa { get; set; }
        public string teleponMahasiswa { get; set; }
        public string status { get; set; }
        public int idSkripsi { get; set; }
        public int idTopik { get; set; }
        [Display(Name = "Topik")]
        public string topik { get; set; }
        public string NIK { get; set; }
        //public string namaDosen { get; set; }
        //public string emailDosen { get; set; }
        //public int idSemester { get; set; }
        public String semester { get; set; }
    }
}

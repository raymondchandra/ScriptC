using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Telerik;

namespace Proyek_Informatika.Models
{
    public class MahasiswaPeserta
    {
        public string foto { get; set; }
        public string NPM { get; set; }
        public string nama { get; set; }
        public string email { get; set; }
        public string semester { get; set; }
        public string status { get; set; }        
        public string topik { get; set; }
        public int pengambilan_ke { get; set; }
        public string dosen { get; set; }
        public float nilai_akhir { get; set; }
    }

    public class DosenPembimbing
    {
        public string NIK { get; set; }
        public string nama { get; set; }
        public string email { get; set; }
        public string NPM_mhs { get; set; }
        public string nama_mhs { get; set; }
        public string topik { get; set; }
        public string status { get; set; }
    }

    public class Akun
    {
        public string username { get; set; }
        public string aktif { get; set; }
        public string last_login { get; set; }
        public string peran { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyek_Informatika.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class pesanan_bimbingan
    {
        public int id { get; set; }
        public Nullable<int> setuju { get; set; }
        public string NIK_dosen { get; set; }
        public string NPM_mahasiswa { get; set; }
        public System.DateTime tanggal_mulai { get; set; }
        public Nullable<System.DateTime> tanggal_selesai { get; set; }
        public string text { get; set; }
        public string description { get; set; }
    
        public virtual dosen dosen { get; set; }
        public virtual mahasiswa mahasiswa { get; set; }
    }
}

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
    
    public partial class jadwal_kosong
    {
        public int id { get; set; }
        public string hari { get; set; }
        public System.TimeSpan jam_mulai { get; set; }
        public Nullable<System.TimeSpan> jam_selesai { get; set; }
        public string NIK_dosen { get; set; }
        public string NPM_mahasiswa { get; set; }
    
        public virtual dosen dosen { get; set; }
        public virtual mahasiswa mahasiswa { get; set; }
    }
}

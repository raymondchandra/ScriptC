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
    
    public partial class mahasiswa
    {
        public mahasiswa()
        {
            this.jadwal_kosong = new HashSet<jadwal_kosong>();
            this.pesanan_bimbingan = new HashSet<pesanan_bimbingan>();
            this.skripsis = new HashSet<skripsi>();
        }
    
        public string NPM { get; set; }
        public string nama { get; set; }
        public string email { get; set; }
        public string nomor_telepon { get; set; }
        public string foto { get; set; }
        public string username { get; set; }
    
        public virtual account account { get; set; }
        public virtual ICollection<jadwal_kosong> jadwal_kosong { get; set; }
        public virtual ICollection<pesanan_bimbingan> pesanan_bimbingan { get; set; }
        public virtual ICollection<skripsi> skripsis { get; set; }
    }
}

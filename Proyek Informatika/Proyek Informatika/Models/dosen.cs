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
    
    public partial class dosen
    {
        public dosen()
        {
            this.jadwal_kosong = new HashSet<jadwal_kosong>();
            this.pesanan_bimbingan = new HashSet<pesanan_bimbingan>();
            this.nilais = new HashSet<nilai>();
            this.skripsis = new HashSet<skripsi>();
            this.topiks = new HashSet<topik>();
        }
    
        public string NIK { get; set; }
        public string nama { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    
        public virtual akun akun { get; set; }
        public virtual ICollection<jadwal_kosong> jadwal_kosong { get; set; }
        public virtual ICollection<pesanan_bimbingan> pesanan_bimbingan { get; set; }
        public virtual ICollection<nilai> nilais { get; set; }
        public virtual ICollection<skripsi> skripsis { get; set; }
        public virtual ICollection<topik> topiks { get; set; }
    }
}

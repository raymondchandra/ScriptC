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
    
    public partial class peran
    {
        public peran()
        {
            this.akuns = new HashSet<akun>();
            this.pengumumen = new HashSet<pengumuman>();
        }
    
        public byte id { get; set; }
        public string nama_peran { get; set; }
    
        public virtual ICollection<akun> akuns { get; set; }
        public virtual ICollection<pengumuman> pengumumen { get; set; }
    }
}

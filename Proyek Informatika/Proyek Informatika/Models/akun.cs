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
    
    public partial class akun
    {
        public akun()
        {
            this.dosens = new HashSet<dosen>();
            this.mahasiswas = new HashSet<mahasiswa>();
            this.calendar_event = new HashSet<calendar_event>();
        }
    
        public string username { get; set; }
        public string password { get; set; }
        public byte aktif { get; set; }
        public Nullable<System.DateTime> last_login { get; set; }
        public string peran { get; set; }
    
        public virtual ICollection<dosen> dosens { get; set; }
        public virtual peran peran1 { get; set; }
        public virtual ICollection<mahasiswa> mahasiswas { get; set; }
        public virtual ICollection<calendar_event> calendar_event { get; set; }
    }
}

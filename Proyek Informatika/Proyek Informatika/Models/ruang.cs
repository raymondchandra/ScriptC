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
    
    public partial class ruang
    {
        public ruang()
        {
            this.sidangs = new HashSet<sidang>();
        }
    
        public string id { get; set; }
        public string nama_ruang { get; set; }
    
        public virtual ICollection<sidang> sidangs { get; set; }
    }
}

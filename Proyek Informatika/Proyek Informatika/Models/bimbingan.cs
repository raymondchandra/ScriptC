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
    using System.Runtime.Serialization;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public partial class bimbingan
    {
        [ScaffoldColumn(false)]
        public int id { get; set; }

        [DisplayName("Pokok Bahasan")]
        public string isi { get; set; }

        [DisplayName("Deskripsi")]
        [DataType(DataType.MultilineText)]
        public string deskripsi { get; set; }

        [ScaffoldColumn(false)]
        public int id_skripsi { get; set; }

        [ScaffoldColumn(true)]
        [ReadOnly(true)]
        [Editable(false)]
        [DataType(DataType.Date)]
        public System.DateTime tanggal { get; set; }

        public virtual skripsi skripsi { get; set; }
    }
}

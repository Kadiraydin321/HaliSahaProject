//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HaliSahaProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Rezervations
    {
        public int ID { get; set; }
        [DisplayName("Tarih")]
        public Nullable<System.DateTime> Date { get; set; }
        [DisplayName("Durum")]
        public Nullable<bool> State { get; set; }
        [DisplayName("Hal� Saha")]
        public Nullable<int> Astroturf_ID { get; set; }
        [DisplayName("Kullan�c�")]
        public Nullable<int> User_ID { get; set; }
    
        public virtual Astroturfs Astroturfs { get; set; }
        public virtual Users Users { get; set; }
    }
}

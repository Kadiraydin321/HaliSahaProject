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

    public partial class Admin
    {
        [DisplayName("Kullan�c� ID")]
        public int User_ID { get; set; }
        [Required]
        [DisplayName("Kullan�c� Ad�")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("�ifre")]
        public string Password { get; set; }
    }
}

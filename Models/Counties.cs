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

    public partial class Counties
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Counties()
        {
            this.Astroturfs = new HashSet<Astroturfs>();
        }
    
        public int ID { get; set; }
        [DisplayName("?l?e")]
        public string Name { get; set; }
        public Nullable<int> City_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Astroturfs> Astroturfs { get; set; }
        public virtual Cities Cities { get; set; }
    }
}

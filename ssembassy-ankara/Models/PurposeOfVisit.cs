//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ssembassy_ankara.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurposeOfVisit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurposeOfVisit()
        {
            this.CitizenRegistration = new HashSet<CitizenRegistration>();
            this.OnlineVisaApplication = new HashSet<OnlineVisaApplication>();
        }
    
        public int Id { get; set; }
        public string Purpose { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CitizenRegistration> CitizenRegistration { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineVisaApplication> OnlineVisaApplication { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dealer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cant_Pasajeros
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cant_Pasajeros()
        {
            this.Automovils = new HashSet<Automovil>();
        }
    
        public int ID_CanPasajeros { get; set; }
        public int CanPasajeros { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Automovil> Automovils { get; set; }
    }
}

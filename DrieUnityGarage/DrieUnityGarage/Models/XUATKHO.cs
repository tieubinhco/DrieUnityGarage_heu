//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DrieUnityGarage.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class XUATKHO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XUATKHO()
        {
            this.CT_XUATKHO = new HashSet<CT_XUATKHO>();
        }
    
        public string MaXK { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public string XK_MaNV { get; set; }
        public string LyDoXuat { get; set; }
        public string XK_MaBG { get; set; }
        public string SoChungTu { get; set; }
    
        public virtual BAOGIA BAOGIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_XUATKHO> CT_XUATKHO { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}

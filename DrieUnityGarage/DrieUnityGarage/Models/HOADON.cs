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
    using System.Linq;

    public partial class HOADON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        DrieUnityGarageEntities db = new DrieUnityGarageEntities();
        public HOADON()
        {
            this.THONGTINTHANHTOANs = new HashSet<THONGTINTHANHTOAN>();
        }
        public HOADON(String id)
        {
            this.HD_MaKH = HD_MaKH;
        }
        public string MaHD { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public string HD_MaKH { get; set; }
        public string HD_BienSoXe { get; set; }
        public Nullable<decimal> TongThanhToan { get; set; }
        public string HD_MaTT { get; set; }
        public string HD_MaTN { get; set; }
    
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual PHUONGTIEN PHUONGTIEN { get; set; }
        public virtual THONGTINTHANHTOAN THONGTINTHANHTOAN { get; set; }
        public virtual THONGTINTIEPNHAN THONGTINTIEPNHAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THONGTINTHANHTOAN> THONGTINTHANHTOANs { get; set; }
    }
}

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
    
    public partial class BANGIAOXE
    {
        public string MaBG { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public string BG_MaTN { get; set; }
        public Nullable<int> DanhGiaTinhTrang { get; set; }
        public Nullable<int> DanhGiaTrangBi { get; set; }
    
        public virtual THONGTINTIEPNHAN THONGTINTIEPNHAN { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KalleMaint.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSpil
    {
        public int spilPK { get; set; }
        public int spilSpillerID { get; set; }
        public System.DateTime spilDato { get; set; }
        public int spilScore1 { get; set; }
    
        public virtual tblSpiller tblSpiller { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service.PolicyService.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class RiskInsured
    {
        public int PolicyNumber { get; set; }
        public int Construction { get; set; }
        public System.DateTime YearBuilt { get; set; }
        public int ADDRESSID { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual ConstructionType ConstructionType { get; set; }
        public virtual Policy Policy { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOP2.HR.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Department
    {
        public Department()
        {
            this.EMPs = new HashSet<EMP>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<EMP> EMPs { get; set; }
    }
}
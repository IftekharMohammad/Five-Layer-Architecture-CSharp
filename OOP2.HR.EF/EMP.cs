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
    
    public partial class EMP
    {
        public string id { get; set; }
        public string password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public System.DateTime dob { get; set; }
        public string dept { get; set; }
        public double salary { get; set; }
        public int DeptartmentID { get; set; }
    
        public virtual Department Department { get; set; }
    }
}

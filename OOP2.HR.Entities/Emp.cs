using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.HR.Entities
{
    public class Emp
    {
        public string id { get; set; }
        public string password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public DateTime dob { get; set; }
        public string dept { get; set; }
        public float salary { get; set; }

		//not mapped property

	    public string Name
	    {
		    get { return this.fname + " " + this.lname; }
	    }

	    public string dobString
	    {
		    get { return this.dob.ToString("d"); }
	    }
    }
}

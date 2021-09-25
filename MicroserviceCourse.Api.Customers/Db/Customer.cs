using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Customers.Db
{
    public class Customer
    {
        public int id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }

        public string  email { get; set; }

        public string address { get; set; }
    }
}

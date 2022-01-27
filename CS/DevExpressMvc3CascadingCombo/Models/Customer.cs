using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CS.Models {
    public class Customer {
        public Customer() {
            ID = -1;
            Country = -1;
            City = -1;
        }

        public int ID { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
    }
}

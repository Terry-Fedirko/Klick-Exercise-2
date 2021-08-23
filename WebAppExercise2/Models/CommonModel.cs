using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CommonModel
    {
        public string FirstName { get; set; } = "Terry";
        public string LastName { get; set; } = "Fedirko";
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

    }
}
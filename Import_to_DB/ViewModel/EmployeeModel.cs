using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Import_to_DB.ViewModel
{
    public class EmployeeModel
    {
            public int ID { get; internal set; }

            public string Payroll { get; set; }

            public string Firstname { get; set; }

            public string Surname { get; set; }

            public System.DateTime Birthday { get; set; }

            public string Telephone { get; set; }

            public string Mobile { get; set; }

            public string Address { get; set; }

            public string Address_2 { get; set; }

            public string Postcode { get; set; }

            public string Email { get; set; }

            public DateTime StartDate { get; set; }

    }
}
using Import_to_DB.Models;
using Import_to_DB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Import_to_DB.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();

            using (DatabaseEntities dc = new DatabaseEntities())
            {

                var v = (from a in dc.Employees
                         select new EmployeeModel
                         {
                             ID = a.Id,
                             Payroll = a.Payroll,
                             Firstname = a.Firstname,
                             Surname = a.Surname,
                             Birthday = a.Birthday,
                             Telephone = a.Telephone,
                             Mobile = a.Mobile,
                             Address = a.Address,
                             Address_2 = a.Address_2,
                             Postcode = a.Postcode,
                             Email = a.Email,
                             StartDate = a.Start_date

                         });
                list = v.ToList();

            }

                return View(list);
        }
    }

}
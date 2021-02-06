using Import_to_DB.Models;
using Import_to_DB.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Import_to_DB.Controllers
{
    public class HomeController : Controller
    {
        // GET: Index
        [Obsolete]
        public ActionResult Index(string search = "")
        {
            List<EmployeeModel> list = new List<EmployeeModel>();

            using (DatabaseEntities dc = new DatabaseEntities())
            {

                var v = (from a in dc.Employees
                         select new EmployeeModel 
                         {
                             Id = a.Id,
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

                if (!string.IsNullOrWhiteSpace(search))
                {
                    v = v.Where(p => p.Firstname.Contains(search) || p.Surname.Contains(search) || p.Payroll.Contains(search)
                    || p.Telephone.Contains(search) || p.Mobile.Contains(search) || p.Address.Contains(search) || p.Address_2.Contains(search)
                    || p.Postcode.Contains(search) || p.Email.Contains(search));

                    ViewBag.Search = search;
                }
                list = v.OrderBy(p => p.Surname).ToList();

            }

                return View(list);
        }

        public ActionResult UpdateEmployee(Employee employee)
        {
            using (DatabaseEntities entities = new DatabaseEntities())
            {
                Employee updatedEmployee = (from c in entities.Employees
                                            where c.Id == employee.Id
                                            select c).FirstOrDefault();

                updatedEmployee.Payroll = employee.Payroll;
                updatedEmployee.Firstname = employee.Firstname;
                updatedEmployee.Surname = employee.Surname;
                updatedEmployee.Birthday = employee.Birthday.Date;
                updatedEmployee.Telephone = employee.Telephone;
                updatedEmployee.Mobile = employee.Mobile;
                updatedEmployee.Address = employee.Address;
                updatedEmployee.Address_2 = employee.Address_2;
                updatedEmployee.Postcode = employee.Postcode;
                updatedEmployee.Email = employee.Email;
                updatedEmployee.Start_date = employee.Start_date.Date;

                entities.SaveChanges();
            }

            return new EmptyResult();
        }

    }

}
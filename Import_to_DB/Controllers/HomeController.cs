using Import_to_DB.Models;
using Import_to_DB.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Import_to_DB.Controllers
{
    public class HomeController : Controller
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
                updatedEmployee.Birthday = employee.Birthday;
                updatedEmployee.Telephone = employee.Telephone;
                updatedEmployee.Mobile = employee.Mobile;
                updatedEmployee.Address = employee.Address;
                updatedEmployee.Address_2 = employee.Address_2;
                updatedEmployee.Postcode = employee.Postcode;
                updatedEmployee.Email = employee.Email;
                updatedEmployee.Start_date = employee.Start_date;

                entities.SaveChanges();
            }

            return new EmptyResult();
        }

    }

}
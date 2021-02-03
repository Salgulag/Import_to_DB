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
    public class FileController : Controller
    {
        // Post: File
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);

                    //Validate uploaded file and return error.
                    if (fileExtension != ".csv")
                    {
                        ViewBag.Message = "Please select the correct file with .csv extension";
                        return View();
                    }


                    var employees = new List<EmployeeModel>();
                    using (var sreader = new StreamReader(postedFile.InputStream))
                    {
                        //First line is header. If header is not passed in csv then we can neglect the below line.
                        string[] headers = sreader.ReadLine().Split(',');
                        //Loop through the records
                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');

                            employees.Add(new EmployeeModel
                            {
                                Payroll = rows[0].ToString(),
                                Firstname = rows[1].ToString(),
                                Surname = rows[2].ToString(),
                                Birthday = DateTime.ParseExact(rows[3].ToString(), "d/M/yyyy", null),
                                Telephone = rows[4].ToString(),
                                Mobile = rows[5].ToString(),
                                Address = rows[6].ToString(),
                                Address_2 = rows[7].ToString(),
                                Postcode = rows[8].ToString(),
                                Email = rows[9].ToString(),
                                StartDate = DateTime.ParseExact(rows[10].ToString(), "d/M/yyyy", null)
                            });
                        }

                        using (DatabaseEntities db = new DatabaseEntities())
                        {
                            var toInsert = employees.Where(p => !db.Employees.Any(x => x.Payroll == p.Payroll));
                            db.Employees.AddRange(toInsert.Select(a => new Employee()
                            {
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
                                Start_date = a.StartDate
                            }));
                            db.SaveChanges();
                            ViewBag.Message = $"Successfully inserted {toInsert.Count()} entries";
                        }
                    }

                    // return Json("Index", "Home", employees);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please select the file first to upload.";
            }

            return View();

        }
    }
}
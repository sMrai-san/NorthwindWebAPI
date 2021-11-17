using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //****************************************************************************************************************************
        //GET ALL EMPLOYEES
        //****************************************************************************************************************************

        [HttpGet]
        [Route("")]
        public List<Employees> GetAllEmployees()
        {
            NORTHWNDContext db = new NORTHWNDContext();
            //List<Employees> allEmployees = db.Employees.ToList();
            //return allEmployees;

            return db.Employees.Select(x => new Employees {
                EmployeeId = x.EmployeeId,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Title = x.Title,
                TitleOfCourtesy = x.TitleOfCourtesy,
                BirthDate = x.BirthDate,
                HireDate = x.HireDate,
                Address = x.Address,
                City = x.City,
                Region = x.Region,
                PostalCode = x.PostalCode,
                Country = x.Country,
                HomePhone = x.HomePhone,
                ReportsTo = x.ReportsTo
            }).ToList();

        }

        [HttpGet]
        [Route("getsome")]
        public List<Employees> GetSomeEmployees(int offset, int limit, string lastName)
        //public IActionResult GetSomeEmployees(int offset, int limit, string lastName)
        {
            if (lastName != null)
            {
                //NORTHWNDContext database = new NORTHWNDContext();
                //List<Employees> someEmployees = database.Employees.Where(l => l.LastName == lastName).Take(limit).ToList();
                //return Ok(someEmployees);
                NORTHWNDContext database = new NORTHWNDContext();
                List<Employees> someEmployees = database.Employees.Where(l => l.LastName == lastName).Take(limit).ToList();
                return someEmployees;
            }
            else
            {
                NORTHWNDContext database = new NORTHWNDContext();
                //List<Employees> someEmployees = database.Employees.Skip(offset).Take(limit).ToList();
                //return Ok(someEmployees);

                return database.Employees.Select(x => new Employees
                {
                    EmployeeId = x.EmployeeId,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Title = x.Title,
                    TitleOfCourtesy = x.TitleOfCourtesy,
                    BirthDate = x.BirthDate,
                    HireDate = x.HireDate,
                    Address = x.Address,
                    City = x.City,
                    Region = x.Region,
                    PostalCode = x.PostalCode,
                    Country = x.Country,
                    HomePhone = x.HomePhone,
                    ReportsTo = x.ReportsTo
                }).Skip(offset).Take(limit).ToList();
            }

        }


        //****************************************************************************************************************************
        //GET ONE Employee
        //****************************************************************************************************************************
        [HttpGet]
        [Route("{employeeId}")]
        public Employees GetEmployeesViaId(int employeeId)
        {
            NORTHWNDContext db = new NORTHWNDContext();
            Employees employee = db.Employees.Find(employeeId);
            return employee;

        }

        //****************************************************************************************************************************
        //GET Employees VIA SURNAME
        //****************************************************************************************************************************
        [HttpGet]
        [Route("lastname/{key}")]
        public List<Employees> GetEmployeesViaLastname(string key)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            var empvialastname= from c in db.Employees
                             where c.LastName == key
                             select c;

            return empvialastname.ToList();

        }

        //****************************************************************************************************************************
        //CREATE
        //****************************************************************************************************************************

        [HttpPost] //POST -filter.
        [Route("")]

        public ActionResult PostCreateNew([FromBody] Employees newemployee)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                db.Employees.Add(newemployee);
                db.SaveChanges();
                return Ok("Employee " + newemployee.FirstName + " " + newemployee.LastName + " added.");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong while adding Employee. If the problem persists, contact the administrator.");

            }
            finally
            {
                db.Dispose();
            }
        }

        //****************************************************************************************************************************
        //UPDATE
        //****************************************************************************************************************************

        [HttpPut] //PUT -filter.
        [Route("{key}")]

        public ActionResult PutEdit(int key, [FromBody] Employees editEmployee)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Employees employee = db.Employees.Find(key);
                if (employee != null)
                {
                    employee.FirstName = editEmployee.FirstName;
                    employee.LastName = editEmployee.LastName;
                    employee.TitleOfCourtesy = editEmployee.TitleOfCourtesy;
                    employee.Address = editEmployee.Address;
                    employee.PostalCode = editEmployee.PostalCode;
                    employee.City = editEmployee.City;
                    employee.Country = editEmployee.Country;
                    employee.HomePhone = editEmployee.HomePhone;
                    employee.BirthDate = editEmployee.BirthDate;
                    employee.HireDate = editEmployee.HireDate;
                    employee.Photo = editEmployee.Photo;
                    employee.PhotoPath = editEmployee.PhotoPath;
                    employee.Region = editEmployee.Region;
                    employee.Title = editEmployee.Title;
                    employee.ReportsTo = editEmployee.ReportsTo;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The employee was not found.");
                }
                return Ok("Employee " + employee.FirstName + " " + employee.LastName + " " + employee.EmployeeId + " updated.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while updating employee. If the problem persists, contact the administrator.");

            }
            finally
            {
                db.Dispose();
            }
        }

        //****************************************************************************************************************************
        //DELETE
        //****************************************************************************************************************************


        [HttpDelete] //Delete -filtteri.
        [Route("{key}")]

        public ActionResult DeleteEmployee(int key)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Employees employee = db.Employees.Find(key);
                if (employee != null)
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The employee was not found.");
                }
                return Ok("Employee " + employee.FirstName + " " + employee.LastName + " " + employee.EmployeeId + " was deleted.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while deleting the employee. If the problem persists, contact the administrator.");
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
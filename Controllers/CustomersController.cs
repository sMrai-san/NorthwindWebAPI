using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //****************************************************************************************************************************
        //GET ALL CUSTOMERS
        //****************************************************************************************************************************

        [HttpGet]
        [Route("")]
        public List<Customers> GetAllCustomers()
        {
            NORTHWNDContext database = new NORTHWNDContext();
            List<Customers> allcustomers = database.Customers.ToList();
            return allcustomers;

        }

        [HttpGet]
        [Route("getsome")]
        public IActionResult GetSomeCustomers(int offset, int limit, string country)
        {
            if (country != null)
            {
                NORTHWNDContext database = new NORTHWNDContext();
                List<Customers> somecustomers = database.Customers.Where(c => c.Country == country).Take(limit).ToList();
                return Ok(somecustomers);
            }
            else
            {
                NORTHWNDContext database = new NORTHWNDContext();
                List<Customers> somecustomers = database.Customers.Skip(offset).Take(limit).ToList();
                return Ok(somecustomers);
            }
            

        }

        //****************************************************************************************************************************
        //GET ONE CUSTOMER
        //****************************************************************************************************************************
        [HttpGet]
        [Route("{customerId}")]
        public Customers GetCustomerViaId(string customerId)
        {
            NORTHWNDContext database = new NORTHWNDContext();
            Customers customer = database.Customers.Find(customerId);
            return customer;

        }

        //****************************************************************************************************************************
        //GET CUSTOMERS VIA COUNTRY
        //****************************************************************************************************************************
        [HttpGet]
        [Route("country/{key}")]
        public List<Customers> GetCustomerViaCountry(string key)
        {
            NORTHWNDContext database = new NORTHWNDContext();

            var custviacountry = from c in database.Customers
                                 where c.Country == key
                                 select c;

            return custviacountry.ToList();

        }

        //****************************************************************************************************************************
        //CREATE
        //****************************************************************************************************************************

        [HttpPost] //POST -filter.
        [Route("")]

        public ActionResult PostCreateNew([FromBody] Customers newcust)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                db.Customers.Add(newcust);
                db.SaveChanges();
                return Ok("Customer " + newcust.CustomerId + " added."); //palautetaan juuri tallennettu asiakas
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong adding customer. If the problem persists, contact the administrator.");

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

        public ActionResult PutEdit(string key, [FromBody] Customers editcust)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Customers customer = db.Customers.Find(key);
                if (customer != null)
                {
                    customer.CompanyName = editcust.CompanyName;
                    customer.ContactName = editcust.ContactName;
                    customer.ContactTitle = editcust.ContactTitle;
                    customer.Country = editcust.Country;
                    customer.Address = editcust.Address;
                    customer.City = editcust.City;
                    customer.PostalCode = editcust.PostalCode;
                    customer.Phone = editcust.Phone;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The customer was not found.");
                }
                return Ok("Customer " + customer.CustomerId + " updated.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while updating customer. If the problem persists, contact the administrator.");

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

        public ActionResult DeleteCustomer(string key)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Customers customer = db.Customers.Find(key);
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The customer was not found.");
                }
                return Ok("Customer " + customer.CustomerId + " was deleted.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while deleting the customer. If the problem persists, contact the administrator.");
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
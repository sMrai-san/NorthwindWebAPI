using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        // GET how to
        [HttpGet]
        public string Get()
        {
            return
                "Api calls: \n\n" +
                "[ROOT]/nw/Customers - Lists northwind customers table data\n" +
                "[ROOT]/nw/Employees - Lists northwind Employees table data\n" +
                "[ROOT]/nw/Orders - Lists northwind Orders table data\n" +
                "[ROOT]/nw/Products.. - Lists northwind Products table data \n\n" +
                "[ROOT]/my/mystring/ - returns a string" +
                "[ROOT]/my/myobject/ - returns an object \n" +
                "[ROOT]/my/myobjectlist/ - returns an object list \n" +
                "[ROOT]/my/mydate/ - returns a datetime \n";
        }


        [HttpGet]
        [Route("mystring")]
        public string Merkkijono()
        {
            return "Hello API!";
        }
        //**********************************************************************************
        [HttpGet] //myobject -route
        [Route("myobject")]
        public Person Olio()
        {
            return new Person()
            {
                Name = "John Doe",
                Address = "Street 666",
                Age = 18
            };
                
        }
        //**********************************************************************************
        [HttpGet] //myobjectlist -route
        [Route("myobjectlist")]
        public List<Person> OlioLista()
        {
            List<Person> persons = new List<Person>()
            {
                new Person(){
                Name = "John Doe",
                Address = "Street 666",
                Age = 18
                },
                new Person(){
                Name = "Jane Doe",
                Address = "Street 555",
                Age = 21
                },
                new Person(){
                Name = "Tim Doe",
                Address = "Street 123",
                Age = 15
                }

            };

            return persons;


        }

        //**********************************************************************************
        [HttpGet]
        [Route("mydate")]
        public DateTime pvm()
        {
            return DateTime.Now;
        }


    }
}

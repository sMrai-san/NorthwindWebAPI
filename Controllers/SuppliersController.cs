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
    public class SuppliersController : ControllerBase
    {
        //****************************************************************************************************************************
        //GET ALL SUPPLIERS
        //****************************************************************************************************************************

        [HttpGet]
        [Route("getsupp")]
        public List<Suppliers> GetAllSuppliers()
        {
            NORTHWNDContext db = new NORTHWNDContext();
            List<Suppliers> allSuppliers = db.Suppliers.ToList();
            return allSuppliers;

        }
    }
}
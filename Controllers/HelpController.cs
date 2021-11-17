using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class HelpController : ControllerBase
    {
        //Default
        [HttpGet]
        public string Get()
        {
            return("How to use: help/insertkeycodehere");
        }

        //***********************************************************************************************************
        //GET DATA WITH PASSWORD
        [HttpGet]
        [Route("{password}")]
        public ActionResult GetHelp(string password)
        {
            NORTHWNDContext database = new NORTHWNDContext();
            if (password == "1") //eli jos salasana on 1 niin näytetään taulun sisältö
            {
                var allHelp = (from e in database.Documentation
                               where e.Keycode == password
                               select e);

                return Ok(allHelp.ToArray());
            }
            else
            {

                return BadRequest(DateTime.Now + ": " + "Antamallasi koodilla ei löytynyt yhtään osumaa. Tarkista koodi ja virheen toistuessa olkaa yhteydessä järjestelmävalvojaan. ");

            }


            //***********************************************************************************************************

            //Errorhandling if needed.
            //[HttpGet]
            //[Route("error")]
            //public string Error()
            //{
            //    return DateTime.Now.ToString() + ". " + "Error. If error persists contact site Admin.";
            //}

        }



    }
}



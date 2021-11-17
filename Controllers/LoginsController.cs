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
    public class LoginsController : ControllerBase
    {
        //****************************************************************************************************************************
        //GET ALL LOGINS
        //****************************************************************************************************************************

        [HttpGet]
        [Route("")]
        public List<Logins> GetAllLogins()
        {
            NORTHWNDContext database = new NORTHWNDContext();
            List<Logins> allLogins = database.Logins.ToList();
            return allLogins;

        }

        [HttpGet]
        [Route("getsome")]
        public IActionResult GetSomeLogins(int offset, int limit, string surName)
        {
            if (surName != null)
            {
                NORTHWNDContext database = new NORTHWNDContext();
                List<Logins> someLogins = database.Logins.Where(l => l.SurName == surName).Take(limit).ToList();
                return Ok(someLogins);
            }
            else
            {
                NORTHWNDContext database = new NORTHWNDContext();
                List<Logins> someLogins = database.Logins.Skip(offset).Take(limit).ToList();
                return Ok(someLogins);
            }

        }

        //****************************************************************************************************************************
        //GET ONE LOGIN
        //****************************************************************************************************************************
        [HttpGet]
        [Route("{LoginId}")]
        public Logins GetLoginViaId(string LoginId)
        {
            NORTHWNDContext database = new NORTHWNDContext();
            Logins login = database.Logins.Find(LoginId);
            return login;

        }


        //****************************************************************************************************************************
        //CREATE
        //****************************************************************************************************************************

        [HttpPost] //POST -filter.
        [Route("")]

        public ActionResult PostCreateNew([FromBody] Logins newLogin)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                db.Logins.Add(newLogin);
                db.SaveChanges();
                return Ok("Login " + newLogin.LoginId + " added."); //palautetaan juuri tallennettu asiakas
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong adding login. If the problem persists, contact the administrator.");

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

        public ActionResult PutEdit(int? key, [FromBody] Logins editLogin)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Logins loginEdit = db.Logins.Find(key);
                if (loginEdit != null)
                {
                    loginEdit.UserName = editLogin.UserName;
                    loginEdit.PassWord = editLogin.PassWord;
                    loginEdit.FirstName = editLogin.FirstName;
                    loginEdit.SurName = editLogin.SurName;
                    loginEdit.Email = editLogin.Email;
                    loginEdit.AccesLevelID = editLogin.AccesLevelID;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The login was not found.");
                }
                return Ok("Login " + loginEdit.LoginId + " updated.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while updating login. If the problem persists, contact the administrator.");

            }
            finally
            {
                db.Dispose();
            }
        }

        //****************************************************************************************************************************
        //DELETE
        //****************************************************************************************************************************


        [HttpDelete] //Delete -filter
        [Route("{key}")]

        public ActionResult DeleteLogin(int? key)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Logins deleteLogin = db.Logins.Find(key);
                if (deleteLogin != null)
                {
                    db.Logins.Remove(deleteLogin);
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The login was not found.");
                }
                return Ok("Login " + deleteLogin.LoginId+ " was deleted.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while deleting the login. If the problem persists, contact the administrator.");
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
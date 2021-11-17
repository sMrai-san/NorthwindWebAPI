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
    public class OrdersController : ControllerBase
    {
        //****************************************************************************************************************************
        //GET ALL ORDERS
        //****************************************************************************************************************************

        [HttpGet]
        [Route("")]
        public List<Orders> GetAllProducts()
        {
            NORTHWNDContext db = new NORTHWNDContext();
            List<Orders> allOrders = db.Orders.ToList();
            return allOrders;

        }


        //****************************************************************************************************************************
        //GET ONE ORDER
        //****************************************************************************************************************************
        [HttpGet]
        [Route("{orderId}")]
        public Orders GetOrderViaId(int orderId)
        {
            NORTHWNDContext db = new NORTHWNDContext();
            Orders order = db.Orders.Find(orderId);
            return order;

        }

        //****************************************************************************************************************************
        //GET ORDERS VIA CUSTOMERID
        //****************************************************************************************************************************
        [HttpGet]
        [Route("customer/{key}")]
        public List<Orders> GetProductsViaCustomerId(string key)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            var ordviacust = from c in db.Orders
                             where c.CustomerId == key
                             select c;

            return ordviacust.ToList();

        }

        //****************************************************************************************************************************
        //CREATE ORDER
        //****************************************************************************************************************************

        [HttpPost]
        [Route("")]

        public ActionResult PostCreateNewOrder([FromBody] Orders neworder)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                db.Orders.Add(neworder);
                db.SaveChanges();
                return Ok("Order " + neworder.OrderId + " added. For customer (" + neworder.CustomerId + ") " + neworder.Customer + " Order date: " + neworder.OrderDate);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong while adding the order. If the problem persists, contact the administrator.");

            }
            finally
            {
                db.Dispose();
            }
        }

        //****************************************************************************************************************************
        //UPDATE ORDER
        //****************************************************************************************************************************

        [HttpPut] //PUT -filter.
        [Route("{key}")]

        public ActionResult PutEditOrder(int key, [FromBody] Orders editorder)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Orders orders = db.Orders.Find(key);
                if (orders != null)
                {
                    orders.CustomerId = editorder.CustomerId;
                    orders.EmployeeId = editorder.EmployeeId;
                    orders.OrderDate = editorder.OrderDate;
                    orders.RequiredDate = editorder.RequiredDate;
                    orders.ShippedDate = editorder.ShippedDate;
                    orders.ShipVia = editorder.ShipVia;
                    orders.Freight = editorder.Freight;
                    orders.ShipName = editorder.ShipName;
                    orders.ShipAddress = editorder.ShipAddress;
                    orders.ShipCity = editorder.ShipCity;
                    orders.ShipRegion = editorder.ShipRegion;
                    orders.ShipPostalCode = editorder.ShipPostalCode;
                    orders.ShipCountry = editorder.ShipCountry;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The order was not found.");
                }
                return Ok("Order " + orders.OrderId + " updated.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while updating the order. If the problem persists, contact the administrator.");

            }
            finally
            {
                db.Dispose();
            }
        }

        //****************************************************************************************************************************
        //DELETE ORDER
        //****************************************************************************************************************************


        [HttpDelete] //Delete -filtteri.
        [Route("{key}")]

        public ActionResult DeleteOrder(string key)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Orders order = db.Orders.Find(key);
                if (order != null)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The order was not found.");
                }
                return Ok("Order (" + order.OrderId + ") was deleted.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while deleting the order. If the problem persists, contact the administrator.");
            }
            finally
            {
                db.Dispose();
            }
        }
    }

}
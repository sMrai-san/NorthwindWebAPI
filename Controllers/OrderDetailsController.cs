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
    public class OrderDetailsController : ControllerBase
    {
        //****************************************************************************************************************************
        //GET ALL ORDER DETAILS
        //****************************************************************************************************************************

        [HttpGet]
        [Route("")]
        public List<OrderDetails> GetAllProducts()
        {
            NORTHWNDContext db = new NORTHWNDContext();
            List<OrderDetails> allOrderDetails = db.OrderDetails.ToList();
            return allOrderDetails;

        }


        //****************************************************************************************************************************
        //GET ONE ORDER DETAILS
        //****************************************************************************************************************************
        [HttpGet]
        [Route("{orderdetId}")]
        public OrderDetails GetOrderViaId(int orderdetId)
        {
            NORTHWNDContext db = new NORTHWNDContext();
            OrderDetails orderdet = db.OrderDetails.Find(orderdetId);
            return orderdet;

        }

        //****************************************************************************************************************************
        //GET ORDER DETAILS VIA ORDERID
        //****************************************************************************************************************************
        [HttpGet]
        [Route("orderid/{key}")]
        public ActionResult<List<OrderDetails>> GetOrderdetailsViaOrderid(int key)
        {
            try
            {
                NORTHWNDContext db = new NORTHWNDContext();

                var orddetviaordid = from c in db.OrderDetails
                                     where c.OrderId == key
                                     select c;

                return orddetviaordid.ToList();
            }

            catch (Exception)
            {
                 return BadRequest("Something went wrong while fetching the order details. If the problem persists, contact the administrator.");

            }

        }

        //****************************************************************************************************************************
        //CREATE ORDER DETAILS
        //****************************************************************************************************************************

        [HttpPost] 
        [Route("")]

        public ActionResult PostCreateNewOrderDetail([FromBody] OrderDetails neworderdetail)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                db.OrderDetails.Add(neworderdetail);
                db.SaveChanges();
                return Ok("Order details added for order (" + neworderdetail.OrderId + ")");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong while adding the order details. If the problem persists, contact the administrator.");

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

        public ActionResult PutEditOrderDetails(int key, [FromBody] OrderDetails editorderdet)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                OrderDetails orderdet = (from c in db.OrderDetails where c.OrderId == key && c.ProductId == editorderdet.ProductId select c).FirstOrDefault();
                //OrderDetails orderdet = db.OrderDetails.Find(key);
                if (orderdet != null)
                {
                    orderdet.ProductId = editorderdet.ProductId;
                    orderdet.UnitPrice = editorderdet.UnitPrice;
                    orderdet.Quantity = editorderdet.Quantity;
                    orderdet.Discount = editorderdet.Discount;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The order was not found.");
                }
                return Ok("Order details for (" + orderdet.OrderId + ") updated.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while updating the order details. If the problem persists, contact the administrator.");

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

        public ActionResult DeleteOrderDetails(string key)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                OrderDetails orderdet = db.OrderDetails.Find(key);
                if (orderdet != null)
                {
                    db.OrderDetails.Remove(orderdet);
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The order details was not found.");
                }
                return Ok("Order details for order: (" + orderdet.OrderId + ") was deleted.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while deleting the order details. If the problem persists, contact the administrator.");
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
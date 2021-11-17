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
    public class ProductsController : ControllerBase
    {
        //****************************************************************************************************************************
        //GET ALL PRODUCTS
        //****************************************************************************************************************************

        [HttpGet]
        [Route("")]
        public List<Products> GetAllProducts()
        {
            NORTHWNDContext db = new NORTHWNDContext();
            List<Products> allproducts = db.Products.ToList();
            return allproducts;

        }

        [HttpGet]
        [Route("getsome")]
        public IActionResult GetSomeProducts(int offset, int limit, int? category)
        {
            NORTHWNDContext database = new NORTHWNDContext();
            if (category != null)
            {
                var someproducts = from p in database.Products
                                   from c in database.Categories
                                   where p.CategoryId == c.CategoryId
                                   orderby p.ProductId
                                   select new
                                   {
                                       p.ProductId,
                                       p.ProductName,
                                       p.SupplierId,
                                       p.CategoryId,
                                       p.QuantityPerUnit,
                                       p.UnitPrice,
                                       p.UnitsInStock,
                                       p.UnitsOnOrder,
                                       p.ReorderLevel,
                                       p.Discontinued,
                                       p.ImageLink,

                                       c.CategoryName,
                                       c.Description
                                   };
                //List<Products> someproducts = database.Products.Where(c => c.CategoryId == category).Take(limit).ToList();
                //return Ok(someproducts);

                return Ok(someproducts.Where(c => c.CategoryId == category).Take(limit).ToList());
            }
            else
            {
                var someproducts = from p in database.Products
                                   from c in database.Categories where p.CategoryId == c.CategoryId
                                   orderby p.ProductId
                                   select new
                                   {
                                       p.ProductId,
                                       p.ProductName,
                                       p.SupplierId,
                                       p.CategoryId,
                                       p.QuantityPerUnit,
                                       p.UnitPrice,
                                       p.UnitsInStock,
                                       p.UnitsOnOrder,
                                       p.ReorderLevel,
                                       p.Discontinued,
                                       p.ImageLink,

                                       c.CategoryName,
                                       c.Description
                                   };
                //List<Products> someproducts = database.Products.Skip(offset).Take(limit).ToList();
                return Ok(someproducts.Skip(offset).Take(limit).ToList());
            }


        }
        [HttpGet]
        [Route("getcat")]
        public List<Categories> GetAllCategories()
        {
            NORTHWNDContext db = new NORTHWNDContext();
            List<Categories> allcategories = db.Categories.ToList();
            return allcategories;

            //var categoryid = db.Categories
            //    .Select(x => x.CategoryId).ToArray();

            //return categoryid;

        }
        [HttpGet]
        [Route("getsupplier")]
        public List<Suppliers> GetAllSuppliers()
        {
            NORTHWNDContext db = new NORTHWNDContext();
            List<Suppliers> allsuppliers = db.Suppliers.ToList();
            return allsuppliers;

            //var categoryid = db.Categories
            //    .Select(x => x.CategoryId).ToArray();

            //return categoryid;

        }


        //****************************************************************************************************************************
        //GET ONE PRODUCT
        //****************************************************************************************************************************
        [HttpGet]
        [Route("{productId}")]
        public Products GetProductsViaId(int productId)
        {
            NORTHWNDContext db = new NORTHWNDContext();
            Products product = db.Products.Find(productId);
            return product;

        }

        //****************************************************************************************************************************
        //GET PRODUCTS VIA CATEGORYID
        //****************************************************************************************************************************
        [HttpGet]
        [Route("category/{key}")]
        public List<Products> GetProductsViaCategoryId(int key)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            var prodviacat = from c in db.Products
                                 where c.CategoryId == key
                                 select c;

            return prodviacat.ToList();

        }

        //****************************************************************************************************************************
        //CREATE
        //****************************************************************************************************************************

        [HttpPost] //POST -filter
        [Route("")]

        public ActionResult PostCreateNew([FromBody] Products newprod)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                db.Products.Add(newprod);
                db.SaveChanges();
                return Ok("Product " + newprod.ProductName + " added."); //palautetaan juuri tallennettu tuote
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong while adding product. If the problem persists, contact the administrator.");

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

        public ActionResult PutEdit(int key, [FromBody] Products editprod)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Products product = db.Products.Find(key);
                if (product != null)
                {
                    product.ProductName = editprod.ProductName;
                    product.SupplierId = editprod.SupplierId;
                    product.CategoryId = editprod.CategoryId;
                    product.QuantityPerUnit = editprod.QuantityPerUnit;
                    product.UnitPrice = editprod.UnitPrice;
                    product.UnitsInStock = editprod.UnitsInStock;
                    product.Discontinued = editprod.Discontinued;
                    product.UnitsOnOrder = editprod.UnitsOnOrder;
                    product.ReorderLevel = editprod.ReorderLevel;
                    product.ImageLink = editprod.ImageLink;
                    product.Rpaproc = editprod.Rpaproc;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The product was not found.");
                }
                return Ok("Product " + product.ProductName+ " updated.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while updating product. If the problem persists, contact the administrator.");

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

        public ActionResult DeleteProduct(int key)
        {
            NORTHWNDContext db = new NORTHWNDContext();

            try
            {
                Products product = db.Products.Find(key);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                else
                {
                    return NotFound("The product was not found.");
                }
                return Ok("Product " + product.ProductName + " was deleted.");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while deleting the product. If the problem persists, contact the administrator.");
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
using crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crud.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            Product p = new Product();
            ViewBag.result = p.GetAllProduct();

            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            System.Diagnostics.Debug.WriteLine("Product/Add Called");
           
            ViewBag.mode = "Add";
            if (Request["id"]!=null)
            {
               
                ViewBag.mode = "Edit";
                ViewBag.result = new Product().GetProductById(Convert.ToInt32(Request["id"]));
            }
            
            
            return View();
        }
        [HttpPost]
        public void Insert()
        {
            System.Diagnostics.Debug.WriteLine("Product/Insert Called");
            Product p = new Product();
            p.name = Request["name"];
            p.price = Convert.ToDecimal(Request["price"]);
            if(p.Insert(p)==1)
            {
                Response.Write("Insert Success");
            }else
            {
                Response.Write("Insert Error!");
            }
            
            
        }
       
        [HttpPost]
        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("Product/Update Called");
            Product p = new Product();
            p.id = Convert.ToInt32(Request["id"]);
            p.name = Request["name"];
            p.price = Convert.ToDecimal(Request["price"]);
            if(p.Update(p)==1)
            {
                Response.Write("Update Success");
            }else
            {
                Response.Write("Update Error!");
            }
            
        }

        [HttpPost]
        public void Delete()
        {
            System.Diagnostics.Debug.WriteLine("Product/Delete Called");
            if(new Product().Delete(Convert.ToInt32(Request["id"]))==1)
            {
                Response.Write("Delete Success");
            }else
            {
                Response.Write("Delete Error!");
            }
            
        }
        

    }
}
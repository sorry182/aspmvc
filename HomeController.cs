using MortgageCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;

namespace MortgageCalculator.Controllers
{
    public class HomeController : Controller
    {
    
        public ActionResult Index()
        {
            return View();
        }

  

        public ActionResult History()
        {
            if (Session["user"] != null)
            {
                MortgageModel data = new MortgageModel();
                ViewBag.result = data.GetHistory();
                return View();
            }
            else
            {
                
                return Redirect("Index");
            }
            
        }
        [HttpPost]
        public ActionResult Login()
        {
            MortgageModel data = new MortgageModel();
            
            if (data.checkpassword(Request["username"],Request["password"]))
            {
                Session["user"] = Request["username"];

            }else
            {
                ViewBag.login = "incorrect username or password";
                
            }
    
            return Redirect("Index");

        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            Session.Abandon();
            
            return Redirect("Index");
        }
        [HttpPost]
        public void InsertResult()
        {
           
            //create object of model
            MortgageModel data = new MortgageModel();


            //pass data to the model using request object
            data.Date = DateTime.Now;
            data.Principal = Convert.ToInt32(Request["principal"]);
            data.Rate = Convert.ToDecimal(Request["rate"]);
            data.Years = Convert.ToInt32(Request["years"]);
            data.Monthly = Convert.ToDecimal(Request["monthly"]);
            
            //call insertion method
            data.Insert(data);
    
        }
        [HttpPost]
        public void Email()
        {
            MortgageModel data = new MortgageModel();
            data.Date = DateTime.Now;
            data.Principal = Convert.ToInt32(Request["principal"]);
            data.Rate = Convert.ToDecimal(Request["rate"]);
            data.Years = Convert.ToInt32(Request["years"]);
            data.Monthly = Convert.ToDecimal(Request["monthly"]);
            

            Mailer mail = new Mailer();
            mail.to= Request["email"];
            
            Response.Write(mail.sendMail(data));
            
        }

        public void Delete()
        {
            System.Diagnostics.Debug.WriteLine("hello");
            //create object of model
            MortgageModel data = new MortgageModel();
            //string ids = Request["ids"];
            string[] ids = Request.Form.GetValues("ids");
            System.Diagnostics.Debug.WriteLine(ids[0]);
            /*
        System.Diagnostics.Debug.WriteLine(ids);
        string[] ia = ids.Split(',');

        foreach( string i in ia)
        {
            System.Diagnostics.Debug.WriteLine(i);
            int num = Int32.Parse(i);

            data.Delete(num);
        }
        */
            Redirect("History");

        }

    }
}
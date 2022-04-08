using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using StoreFront3.UI.MVC.Models;
using System.Configuration;
using System;

namespace StoreFront3.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            //ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                string body = $"{cvm.Name} has sent you the following message:<br/>" + $"{cvm.Message} <strong>from the email address:</strong> {cvm.Email}.";

                //MailMessage mm = new MailMessage(
                //        ConfigurationManager.AppSettings["EmailUser"].ToString(),
                //        ConfigurationManager.AppSettings["EmailTo"].ToString(), cvm.Subject, body);
                MailMessage mm = new MailMessage("administrator@jazminerizo.com", "jaztec.llc@gmail.com", cvm.Subject, body);

                mm.IsBodyHtml = true;

                mm.Priority = MailPriority.High;

                mm.ReplyToList.Add(cvm.Email);

                SmtpClient client = new SmtpClient("mail.jazminerizo.com");

                //SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"]).ToString());

                //client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"]).ToString(), ConfigurationManager.AppSettings["EmailPass"].ToString());
                client.Credentials = new NetworkCredential("administrator@jazminerizo.com", "P@ssw0rd");


                try
                {
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    ViewBag.CustomerMessage = $"We're sorry your request could not be completed at this time." + $"Please try again later. Error Message: <br/> {ex.StackTrace}";
                    return View(cvm);
                }


                return View("EmailConfirmation", cvm);

            }
                return View(cvm);
        }
       
    }
    
}

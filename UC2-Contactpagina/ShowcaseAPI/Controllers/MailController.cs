using Microsoft.AspNetCore.Mvc;
using ShowcaseAPI.Models;
using System.Net;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowcaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        // POST api/<MailController>
        [HttpPost]
        public ActionResult Post([Bind("FirstName, LastName, Email, Phone, Subject, Message")] Contactform form)
        {

            try
            {
                // Mailtrap SMTP-instellingen
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("0792ae9ae52349", "9efd5994e36cda"),
                    EnableSsl = true
                };

                // E-mail verzenden
                string body = $"{form.Message}\n\nGegevens: \nVoornaam: {form.FirstName}\nAchternaam: {form.LastName}\nE-mail: {form.Email}\nTelefoonnummer: {form.Phone}";
                client.Send(form.Email, "s1196604@student.windesheim.nl", form.Subject, body);
                Console.WriteLine("Sent!");
                return Ok("E-mail succesvol verzonden!");
            }
            catch (SmtpException)
            {
                return NotFound();
            }
        }
    }
}

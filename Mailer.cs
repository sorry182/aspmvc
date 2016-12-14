using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace MortgageCalculator.Models
{
    public class Mailer
    {
        SmtpClient client;
        string from = "from email";
        string pass = "from password";
        string host = "smtp.gmail.com";
        int port = 587;
        MailMessage mailMessage;

        string subject = "Mortgage Calculator";
        string body; 

   
        //public string subject { get; set; }
        public string to { get; set; }

        public void prepbody(MortgageModel model)
        { 
            body += "<h2>Mortgage Calculator</h2>";
            body += @"<table>
                    <tr>
                        <td>Date</td>
                        <td colspan ='2'>" + model.Date + @"</td>
                    </tr>
                    <tr>
                        <td>Mortgage Amount</td>
                        <td>$</td>
                        <td>" + model.Principal + @"</td>
                    </tr>
                    <tr>
                        <td>Amortization </td>
                        <td>Years</td>
                        <td>" + model.Years + @"</td>
                    </tr>
                    <tr>
                        <td>Interest Rate</td>
                        <td>%</td>
                        <td>" + model.Rate + @"</td>
                    </tr>
                    <tr>
                        <td> Monthly</td>
                        <td>$</td>
                        <td>" + model.Monthly + @"</td>
                    </tr>
                </table>";

                        
                    
        }
        
        public string sendMail(MortgageModel model)
        {
            
            
            try
            {
                client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = host;
                client.Port = port;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(from, pass);
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                mailMessage = new MailMessage();
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(from);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                prepbody(model);
                mailMessage.Body = body;
                client.Send(mailMessage);
                return "E-mail sent!";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Could not send the e-mail - error: " + ex.Message);
                return "Could not send the e-mail"; 
                
            }
        }    
                

                

               
            
    }
}
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WordApp
{
    public class SendEmail
    {
        public static string From = "wordtemplateapp@gmail.com";
        protected static string FromPassword = "Tajnehaslo1_";
        public string scheme = HttpContext.Current.Request.Url.Scheme;
        public string hostAndPort = HttpContext.Current.Request.Url.Authority;     
        SmtpClient smtp = new SmtpClient
        {
            Host="smtp.gmail.com",
            Port=587,
            EnableSsl=true,
            DeliveryMethod=SmtpDeliveryMethod.Network,
            UseDefaultCredentials=false,
            Credentials=new NetworkCredential( From , FromPassword )
        };

        public void VerificationEmail( string email , string uniqueCode )
        {           
            string route = "/Users/Verification/";
            string activationLink = $"{scheme}://{hostAndPort}{route}{uniqueCode}"; 

            using (var ActivationMessage = new MailMessage(From, email)
            {
                Subject = "Aktywuj swoje konto",
                Body = "<br/><br/> Twoje konto zostało pomyślnie utorzone, kliknij w poniższy link aby je aktywować.<br/><br/> " +
                " <a href='" + activationLink + "'>" + activationLink + "</a><br/><br/> Jeśli to nie Ty zignoruj tą wiadomość",               
                IsBodyHtml = true
            })
            smtp.Send( ActivationMessage );
        }

        public void ChangePasswordEmail(string email, string uniqueCode)
        {
            string route = "/Users/SetNewPassword/";
            string changePassLink = $"{scheme}://{hostAndPort}Se{route}{uniqueCode}";  

            using ( var ChangePassMessage = new MailMessage( From, email )
            {
                Subject = "Prośba o zamiane hasła",
                Body = "<br/><br/> Właśnie poproszono o zmianę hasła do aplikacji WordTemplatesApp, kliknij w poniższy link aby kontynuować.<br/><br/> " +
                " <a href='" + changePassLink + "'>Zmień hasło</a><br/><br/> Jeśli to nie Ty zignoruj tą wiadomość",
                IsBodyHtml = true
            })
            smtp.Send( ChangePassMessage );
        }

    }
}
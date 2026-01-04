using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OrcEMail.BLL
{
    public class EnviaEMail
    {
        public void Envia(string Email, string Conteudo, string Assunto)
        {
            string Host, Usuario, Senha;
            int Port;
            {
                Host = ConfigurationManager.AppSettings["application.Dominio.Host"];
                Usuario = ConfigurationManager.AppSettings["application.Dominio.Usuario"];
                Senha = ConfigurationManager.AppSettings["application.Dominio.Senha"];
                Port = int.Parse(ConfigurationManager.AppSettings["application.Dominio.Port"]);
            }

            //****************************************************************************************************
            // Montando o E-Mail
            //****************************************************************************************************
            MailMessage email = new MailMessage();
            SmtpClient Smtp = new SmtpClient(Host, Port);
            Smtp.UseDefaultCredentials = false;
            //Smtp.EnableSsl = true; //Habilita SSL
            Smtp.Credentials = new System.Net.NetworkCredential(Usuario, Senha); //Usando sua credencial para poder enviar o email

            try
            {
                email.To.Add(Email);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao adicionar e-mail");
            }

            //****************************************************************************************************
            // Enviando E-Mail
            //****************************************************************************************************
            email.From = new MailAddress(Usuario);
            email.Subject = Assunto;
            email.Body += Conteudo;

            email.IsBodyHtml = true;
            string teste = email.Body.ToString();

            try
            {
                Smtp.Send(email);
            }
            catch (Exception exc)
            {
                throw new Exception("Falha ao enviar e-mail");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Kapta;
using Kapta.Herramientas.Interfaces;
using Xamarin.Forms;

namespace Kapta.Contacto
{

    public class ContactViewModel : BaseViewModel
    {

        #region Properties
        public string IntroducirEmail { get; set; }

        public string IntroducirMensaje { get; set; }

        public string IntroducirAsunto { get; set; }

        public string IntroducirNombre { get; set; }

        #endregion

        #region Commands

        public ICommand SendEmailCommand
        {
            get
            {
                return new RelayCommand(SendEmail);
            }
        }

        private async void SendEmail()
        {

            if (string.IsNullOrEmpty(IntroducirNombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error", "Debe ingresar su Nombre", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(IntroducirEmail))
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error", "Debe ingresar su Email", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(IntroducirAsunto))
            {
                await Application.Current.MainPage.DisplayAlert(
                  "Error", "Debe ingresar el Asunto", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(IntroducirMensaje))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Debe ingresar el Mensaje", "Aceptar");
                return;
            }

            //COMPROBACIÓN DEL EMAIL

            var email = IntroducirEmail;
            var nombre = IntroducirNombre;
            var emailPattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";

            if (!String.IsNullOrWhiteSpace(email) && !(Regex.IsMatch(email, emailPattern)))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Debe ingresar un Email Válido", "Aceptar"
                  );
                return;
            }
            else
            {


                var fromAddress = new MailAddress("pablo.kapta@gmail.com", "KAPTA");
                //var toAddress = new MailAddress(email, nombre);
                var toAddress = new MailAddress("pablo.kapta@gmail.com");
                const string fromPassword = "proyectokapta";
                string subject = "Mensaje de " + IntroducirEmail + " a KAPTA";
                const string body = "¡Gracias por usar nuestra aplicación!";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    //Body = "Hola " + IntroducirNombre.Text + " ¡Gracias enviar un mensaje a KAPTA! Te contestaremos lo antes posible",
                    Body = IntroducirNombre + " con email " + IntroducirEmail + " te ha mandado el siguiente mensaje:"
                    + Environment.NewLine + Environment.NewLine +
                    "ASUNTO: " + IntroducirAsunto +
                    Environment.NewLine + Environment.NewLine +
                    "MENSAJE: " + IntroducirMensaje,
                })

                {
                    smtp.Send(message);
                }
                DependencyService.Get<IMessage>().LongAlert("Email enviado.");
            }
        }


        #endregion
    }
}

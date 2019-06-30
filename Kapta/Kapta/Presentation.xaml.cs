using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kapta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Presentation : ContentPage
    {

        public Presentation()
        {
            InitializeComponent();
            string presentacion = "Hola a todos, mi nombre es Pablo y os presento mi proyecto para el Trabajo Final de Grado, una aplicación móvil programada en Xamarin Forms llamada KAPTA.";
            string presentacion2 = "A continuación os presento un vídeo de presentación de mi compañero Jose María, estudiante de CAFD el cual me ha proporcionado los datos necesarios para poder llevar a cabo el proyecto.";
            HtmlWebViewSource personHtmlSource = new HtmlWebViewSource();
            personHtmlSource.SetBinding(HtmlWebViewSource.HtmlProperty, "HTMLDesc");
            personHtmlSource.Html = @"<html><body > <b> Kapta </b> te permitirá guardar tus lesiones de un modo sencillo. Para ello simplemente tienes que rellenar el formulario que te aparece en en la pestaña de entrenamiento personalizado y listo. <br/></br> También encontrarás ejercicios útiles para tus lesiones y la prevención de aquellas que puedan surgir debido a la practica deportiva. 
            <div style=' position: relative; padding-bottom: 56.25%; padding-top: 25px;'>   <iframe style='position: absolute; top: 30; left: 0; width: 100%; height: 115%;'  src='https://www.youtube.com/embed/sAoRwesqUCU' frameborder='0' allowfullscreen></iframe></div><br>.<br> </body></html>";
            var browser = new WebView();
            browser.Source = personHtmlSource;
            Content = browser;
            /*  var label = new Label
                {

                    Text = "Registra tus lesiones de un modo sencillo, simplemente tienes que rellenar " +
                    "el formulario que te aparece en en la pestaña de entrenamiento personalizado y listo. " +
                    "también encontrarás ejercicios útiles para tus lesiones " +
                    "y la prevención de aquellas que puedan surgir debido a la practica deportiva.\n"

          };*/
            /*  Content = new StackLayout
              {
                  
            Padding = 30,
                  Spacing = 10,
                  Children = {label}
                   
        };*/


        }



    }
}
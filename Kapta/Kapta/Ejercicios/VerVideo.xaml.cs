
using Kapta.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kapta.Ejercicios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerVideo : ContentPage
    {

        public VerVideo(Video video)
        {

            InitializeComponent();
            //this.Video = video;
            //string video = "https://www.youtube.com/embed/xFnHTQn5iqo";

            string nombre = video.NombreVideo;
            // TitleProperty.PropertyName.Insert(1, "ksks");

            string linkk = video.LinkVideo;
            string descripcion = video.Description;
            HtmlWebViewSource personHtmlSource = new HtmlWebViewSource();
            personHtmlSource.SetBinding(HtmlWebViewSource.HtmlProperty, "HTMLDesc");
            personHtmlSource.Html = @"<html ><body style='background-color : #D4E6F1;' ><center><h2>" + nombre + "</h2></center><div style=' position: relative; padding-bottom: 56.25%; padding-top: 25px;'>   <iframe style='position: absolute; top: 0; left: 0; width: 100%; height: 100%;'  src='" + linkk + "' frameborder='0' allowfullscreen></iframe></div><br>" + descripcion + " </body></html>";
            var browser = new WebView();
            browser.Source = personHtmlSource;
            Content = browser;
        }
    }
}
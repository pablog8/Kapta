using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Kapta.Ejercicios
{
    public class TablaEjerciciosCell : ViewCell
    {
        public TablaEjerciciosCell()
        {
            var idEjercicioLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
            };

            idEjercicioLabel.SetBinding(Label.TextProperty, new Binding("IDEjercicio"));

            var NombreEjercicioLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
            };

            NombreEjercicioLabel.SetBinding(Label.TextProperty, new Binding("Nombreejercicio"));

            var SeriesLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 15,
                // FontAttributes = FontAttributes.Bold,
            };

            SeriesLabel.SetBinding(Label.TextProperty, new Binding("Descripcion"));




            var line1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                NombreEjercicioLabel
                },
            };
            var line2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                SeriesLabel,
                },
            };


            View = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {
                line1,line2
                },
            };
        }
    }
}

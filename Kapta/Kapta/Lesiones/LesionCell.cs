using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sales.Lesiones
{
    public class Lesioncell : ViewCell
    {
        public Lesioncell()
        {

            var idLesionLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
            };

            idLesionLabel.SetBinding(Label.TextProperty, new Binding("IDLesion"));

            var LugarLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
            };

            LugarLabel.SetBinding(Label.TextProperty, new Binding("Lugar"));

            var TipoLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontSize = 15,
                //FontAttributes = FontAttributes.Bold,
            };

            TipoLabel.SetBinding(Label.TextProperty, new Binding("Tipo"));


            var NumerolesionesLabel = new Label
            {
                //  HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.End,
                FontSize = 15,
                // FontAttributes = FontAttributes.Bold,
            };

            NumerolesionesLabel.SetBinding(Label.TextProperty, new Binding("NumLesiones"));

            var NumeroolesionesLabel = new Label
            {
                Text = "Nº de lesiones: ",
                //HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.End,
                FontSize = 15,
                // FontAttributes = FontAttributes.Bold,
            };
            var TipooLabel = new Label
            {
                Text = "Tipo: ",
                // HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 15,
                // FontAttributes = FontAttributes.Bold,
            };
            var EspacioLabel = new Label
            {
                Text = "  ",
                // HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 15,
                // FontAttributes = FontAttributes.Bold,
            };
            var line1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                LugarLabel
                },
            };
            var line2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                TipooLabel,TipoLabel,NumeroolesionesLabel,NumerolesionesLabel,EspacioLabel
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
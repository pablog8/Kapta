using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Kapta.Deportistas
{
    public class DeportistaCell : ViewCell
    {
        public DeportistaCell()
        {
            var idDeportistaLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
            };

            idDeportistaLabel.SetBinding(Label.TextProperty, new Binding("IDDeportista"));

            var nombreCompetoLabel = new Label
            {
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            nombreCompetoLabel.SetBinding(Label.TextProperty, new Binding("NombreCompleto"));

            var emailtoLabel = new Label
            {
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            emailtoLabel.SetBinding(Label.TextProperty, new Binding("Email"));



            var fechaNacimientoLabel = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            fechaNacimientoLabel.SetBinding(Label.TextProperty, new Binding("FechaNacimientoEditada"));

            /*var salarioLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            salarioLabel.SetBinding(Label.TextProperty, new Binding("SalarioEditado"));*/

            var activoSwitch = new Switch
            {
                IsEnabled = false,
                HorizontalOptions = LayoutOptions.End
            };
            activoSwitch.SetBinding(Switch.IsToggledProperty, new Binding("Activo"));

            var line1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                nombreCompetoLabel
                },
            };
            var line2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                fechaNacimientoLabel, activoSwitch,
                },
            };
            View = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {
                line1, line2,
            },
            };
        }
    }
}

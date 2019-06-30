using Kapta.Herramientas.Interfaces;
using Kapta.Herramientas.Services;
using Sales.Lesiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kapta.Lesiones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditLesion : ContentPage
    {
        Lesion lesionn;
        public EditLesion(Lesion lesion)
        {
            InitializeComponent();
            this.lesionn = lesion;

            this.Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10, 10, 10, 10),
            new Thickness(10, 10, 10, 10));

            pickerlugar.Text = lesion.Lugar;
            pickertipo.Text = lesion.Tipo;
            numeroLesionesEntry.Text = lesion.NumLesiones.ToString();


            actualizarlesion.Clicked += Actualizarlesion_Clicked;
            borrarlesion.Clicked += Borrarlesion_Clicked;
        }

        private async void Actualizarlesion_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(numeroLesionesEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar el numero de lesiones", "Aceptar");
                numeroLesionesEntry.Focus();
                return;
            }
            lesionn.NumLesiones = int.Parse(numeroLesionesEntry.Text);
            using (var datos = new DataAccess())
            {
                datos.UpdateLesion(lesionn);
            }
            DependencyService.Get<IMessage>().LongAlert("Lesión actualizada");
            //await DisplayAlert("Confirmación", "Lesion actualizada correctamente", "Aceptar");
        }

        private async void Borrarlesion_Clicked(object sender, EventArgs e)
        {
            var rta = await DisplayAlert("Confirmación", "¿Desea borrar la lesión?", "Si", "No");
            if (!rta)
            {
                return;
            }
            using (var datos = new DataAccess())
            {
                datos.DeleteLesion(lesionn);
            }

            DependencyService.Get<IMessage>().LongAlert("Lesión eliminada");
            //await DisplayAlert("Confirmación", "Deportista borrado correctamente", "Aceptar");
            //await Navigation.PushAsync(new HomePage());
            await Navigation.PopAsync();
        }
    }
}
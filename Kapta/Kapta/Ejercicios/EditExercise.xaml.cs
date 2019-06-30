
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

namespace Kapta.Ejercicios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditExercise : ContentPage
    {
        private TablaEjercicios ejercicio;
        public EditExercise(TablaEjercicios ej)
        {
            InitializeComponent();

            this.ejercicio = ej;

            this.Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10, 10, 10, 10),
            new Thickness(10, 10, 10, 10));

            nombreejercicioEntry.Text = ejercicio.Nombreejercicio;
            descripcionEntry.Text = ejercicio.Descripcion;
            comentarioEntry.Text = ejercicio.ComentarioEjercicio;


            actualizarButton.Clicked += ActualizarButton_Clicked;
            borrarButton.Clicked += BorrarButton_Clicked;

            // lesionesButton.Clicked += LesionesButton_Clicked;


            // listaaListView.ItemTemplate = new DataTemplate(typeof(Lesioncell));
            // listaaListView.RowHeight = 50;
        }/*
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var datos = new DataAccess())
            {
                listaaListView.ItemsSource = datos.GetLesionDeportista(this.deportista.IDDeportista);
            }
        }*/

        private async void BorrarButton_Clicked(object sender, EventArgs e)
        {
            var rta = await DisplayAlert("Confirmación", "¿Desea borrar el ejercicio?", "Si", "No");
            if (!rta)
            {
                return;
            }
            using (var datos = new DataAccess())
            {
                datos.DeleteEjercicio(ejercicio);
            }
            DependencyService.Get<IMessage>().LongAlert("Ejercicio eliminado");
            //await DisplayAlert("Confirmación", "Ejercicio borrado correctamente", "Aceptar");
            //await Navigation.PushAsync(new HomePage());
            await Navigation.PopAsync();
        }




        private async void ActualizarButton_Clicked(object sender, EventArgs e)
        {
            /*
            if (string.IsNullOrEmpty(nombreejercicioEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar el nombre del ejercicio", "Aceptar");
                nombreejercicioEntry.Focus();
                return;
            }
            */
            /*
            if (string.IsNullOrEmpty(descripcionEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar descripcion", "Aceptar");
                descripcionEntry.Focus();
                return;
            }
            */

            /*
            if (string.IsNullOrEmpty(salarioEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar salario", "Aceptar");
                salarioEntry.Focus();
                return;
            }*/
            ejercicio.Nombreejercicio = nombreejercicioEntry.Text;
            ejercicio.Descripcion = descripcionEntry.Text;
            ejercicio.ComentarioEjercicio = comentarioEntry.Text;

            using (var datos = new DataAccess())
            {
                datos.UpdateEjercicio(ejercicio);
            }
            DependencyService.Get<IMessage>().LongAlert("Ejercicio actualizado");
            // await DisplayAlert("Confirmación", "Ejercicio actualizado correctamente", "Aceptar");
            //await Navigation.PopAsync();

        }

    }
}
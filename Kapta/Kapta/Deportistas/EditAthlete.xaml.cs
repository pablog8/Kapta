using Kapta.Ejercicios;
using Kapta.Herramientas.Interfaces;
using Kapta.Herramientas.Services;
using Kapta.Lesiones;
using Sales.Lesiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kapta.Deportistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAthlete : ContentPage
    {
        private Deportista deportista;
        public EditAthlete(Deportista deportista)
        {
            InitializeComponent();

            this.deportista = deportista;

            this.Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10, 10, 10, 10),
            new Thickness(10, 10, 10, 10));

            nombresEntry.Text = deportista.Nombres;
            apellidosEntry.Text = deportista.Apellidos;
            emailEntry.Text = deportista.Email;
            telefonoEntry.Text = deportista.Telefono;
            fechaContratoDatePicker.Date = deportista.FechaNacimiento;
            //salarioEntry.Text = deportista.Salario.ToString();
            //activoSwitch.IsToggled = deportista.Activo;

            actualizarButton.Clicked += ActualizarButton_Clicked;
            borrarButton.Clicked += BorrarButton_Clicked;
            lesionesButton.Clicked += LesionesButton_Clicked;
            //ejerciciosButton.Clicked += EjerciciosButton_Clicked;

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
            var rta = await DisplayAlert("Confirmación", "¿Desea borrar el deportista?", "Si", "No");
            if (!rta)
            {
                return;
            }
            using (var datos = new DataAccess())
            {
                datos.DeleteEmpleado(deportista);
            }
            DependencyService.Get<IMessage>().LongAlert("Deportista borrado correctamente");
            //await DisplayAlert("Confirmación", "Deportista borrado correctamente", "Aceptar");
            //await Navigation.PushAsync(new HomePage());
            await Navigation.PopAsync();
        }


        private void LesionesButton_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new LesionesView(this.deportista));
        }
        private void EjerciciosButton_Clicked(object sender, EventArgs e)
        {
            
            ((NavigationPage)this.Parent).PushAsync(new TableExercisesView(this.deportista));
        }

        private async void ActualizarButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nombresEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar nombres", "Aceptar");
                nombresEntry.Focus();
                return;
            }
            if (string.IsNullOrEmpty(apellidosEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar apellidos", "Aceptar");
                apellidosEntry.Focus();
                return;
            }
            if (string.IsNullOrEmpty(emailEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar el email", "Aceptar");
                emailEntry.Focus();
                return;
            }
            var email = emailEntry.Text;
            //var nombre = nombresEntry.Text;
            var emailPattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";

            if (!String.IsNullOrWhiteSpace(email) && !(Regex.IsMatch(email, emailPattern)))
            {
                await DisplayAlert("Error", "Debe ingresar un email válido", "Aceptar");
                emailEntry.Focus();
                return;
            }
            /*
            if (string.IsNullOrEmpty(salarioEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar salario", "Aceptar");
                salarioEntry.Focus();
                return;
            }*/
            deportista.Nombres = nombresEntry.Text;
            deportista.Apellidos = apellidosEntry.Text;
            deportista.Email = emailEntry.Text;
            deportista.Telefono = telefonoEntry.Text;
            // deportista.Salario = decimal.Parse(salarioEntry.Text);
            deportista.FechaNacimiento = fechaContratoDatePicker.Date;
            //deportista.Activo = activoSwitch.IsToggled;

            using (var datos = new DataAccess())
            {
                datos.UpdateDeportista(deportista);
            }

            DependencyService.Get<IMessage>().LongAlert("Deportista actualizado");
            //await DisplayAlert("Confirmación", "Deportista actualizado correctamente", "Aceptar");
            //await Navigation.PopAsync();
        }
    }
}
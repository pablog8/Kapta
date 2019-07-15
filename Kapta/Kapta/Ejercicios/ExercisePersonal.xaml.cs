using Kapta.Deportistas;
using Kapta.Herramientas.Interfaces;
using Kapta.Herramientas.Services;
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
	public partial class ExercisePersonal : ContentPage
	{
        //private TablaEjercicios ejercicio;
        Deportista deportistaa;
        public ExercisePersonal( Deportista deportista)
        {
            InitializeComponent();

            //this.ejercicio = ej;
            this.deportistaa = deportista;

            this.Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10, 10, 10, 10),
            new Thickness(10, 10, 10, 10));

            /*
            nombreejercicioEntry.Text = ejercicio.Nombreejercicio;
            descripcionEntry.Text = ejercicio.Descripcion;
            comentarioEntry.Text = ejercicio.ComentarioEjercicio;
            */

            añadirButton.Clicked += AñadirButton_Clicked;
          //  borrarButton.Clicked += BorrarButton_Clicked;

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
        private async void AñadirButton_Clicked(object sender, EventArgs e)
        {
            
            string nombreejercicio = nombreejercicioEntry.Text;
            string descripcionejercicio = descripcionEntry.Text;
            string comentarioo = comentarioEntry.Text;
            if (string.IsNullOrEmpty(nombreejercicio))
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Debe seleccionar el nombre del ejercicio",
                   "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(descripcionejercicio))
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Debe seleccionar la descripción del ejercicio",
                   "Aceptar");
                return;
            }

            //creamos el deportista
            var ejercicio = new TablaEjercicios
            {
                Nombreejercicio = nombreejercicio,
                Descripcion = descripcionejercicio,
                clavedeportista = deportistaa.IDDeportista,
                ComentarioEjercicio = comentarioo,

        // Salario = decimal.Parse(salarioEntry.Text),

    };

            //insertamos el deportista en la base de datos
            using (var datos = new DataAccess())
            {
                datos.InsertEjercicio(ejercicio);
                //listaListView.ItemsSource = datos.GetDeportistas();
            }

            DependencyService.Get<IMessage>().LongAlert("Ejercicio añadido");
            await App.Navigator.PopAsync(false);
            await App.Navigator.PopAsync(false);
        }
	}
}
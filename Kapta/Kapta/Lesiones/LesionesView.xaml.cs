using Kapta.Deportistas;
using Kapta.Ejercicios;
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
    public partial class LesionesView : ContentPage
    {
        Deportista deportistaa;
        public LesionesView(Deportista deportista)
        {
            InitializeComponent();
            this.deportistaa = deportista;
            //listaListView.RowHeight = 70;

            this.Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10, 10, 10, 10),
            new Thickness(10, 10, 10, 10));


            //labelLesiones.Text = "Cuadro de lesiones de " + Environment.NewLine + deportista.NombreCompleto;
            labelLesiones.Text = deportista.NombreCompleto;
            listaListView.ItemTemplate = new DataTemplate(typeof(Lesioncell));
            listaListView.RowHeight = 70;
            //mostrar la lista con los datos previamente ingresados



            nuevo.Clicked += Nuevo_Clicked;
            enviartabla.Clicked += Enviartabla_Clicked;
            listaListView.ItemSelected += ListaListView_ItemSelected;
        }

        private async void Enviartabla_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TableExercisesView(deportistaa));
            //await Navigation.PushAsync(new TablaEjerciciosView(deportistaa));
            //await Navigation.PushAsync(new TablaEjerciciosView(deportistaa));
        }

        private async void Nuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectMemberView(deportistaa));
        }

        //actualizala lista en la pantalla
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var datos = new DataAccess())
            {
                listaListView.ItemsSource = datos.GetLesionDeportista(this.deportistaa.IDDeportista);
            }
        }
        /*
        private async void ListaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditPage((Deportista)e.SelectedItem));
        }*/
        private async void ListaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditLesion((Lesion)e.SelectedItem));
        }

    }


}
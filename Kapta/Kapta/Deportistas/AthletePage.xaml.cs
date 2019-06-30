using GalaSoft.MvvmLight.Command;
using Kapta.Herramientas.Services;
using Sales.Lesiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kapta.Deportistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AthletePage : ContentPage
    {
        string deportistaa;
        public AthletePage(string deportista)
        {
            InitializeComponent();

            //listaListView.RowHeight = 70;
            this.deportistaa = deportista;
            this.Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10, 10, 10, 10),
            new Thickness(10, 10, 10, 10));



            //  listaListView.ItemTemplate = new DataTemplate(typeof(DeportistaCell));
            listaListView.RowHeight = 70;
            //mostrar la lista con los datos previamente ingresados



            //nuevo.Clicked += Nuevo_Clicked;
            listaListView.ItemSelected += ListaListView_ItemSelected1;
        }

        private async void ListaListView_ItemSelected1(object sender, SelectedItemChangedEventArgs e)
        {
            await App.Navigator.PushAsync(new EditAthlete((Deportista)e.SelectedItem));
        }

        private async void Nuevo_Clicked(object sender, EventArgs e)
        {
            await App.Navigator.PushAsync(new AddAthlete());
        }


        //actualizala lista en la pantalla
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var datos = new DataAccess())
            {
                listaListView.ItemsSource = datos.GetDeportistass(this.deportistaa);
            }
        }

        private async void ListaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditAthlete((Deportista)e.SelectedItem));
        }


    }
}
using Kapta.Categorias;
using Kapta.Deportistas;
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
    public partial class SelectTypeExercise : ContentPage
    {
        Deportista deportistaa;
        public SelectTypeExercise(Deportista deportista)
        {
            InitializeComponent();
            this.deportistaa = deportista;

            //listaListView.RowHeight = 70;

            this.Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10, 10, 10, 10),
            new Thickness(10, 10, 10, 10));


            //labelTabla.Text = "Tabla de ejercicios " + Environment.NewLine + deportistaa.NombreCompleto;
            //  labelTabla.Text = deportistaa.NombreCompleto;
            //listaListView.ItemTemplate = new DataTemplate(typeof(TablaEjerciciosCell));
            // listaListView.RowHeight = 70;
            //mostrar la lista con los datos previamente ingresados



            ejercicioapp.Clicked += Ejercicioapp_Clicked;
            ejerciciopersonalizado.Clicked += Ejerciciopersonalizado_Clicked;
            // listaListView.ItemSelected += ListaListView_ItemSelected;
            //  listaListView.ItemSelected += ListaListView_ItemSelected;
        }
        private async void Ejercicioapp_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Categoriess = new CategoriesViewModelUser(deportistaa);
            //Application.Current.MainPage = new NavigationPage(new LoginPage());
            await App.Navigator.PushAsync(new CategoriesPageUser());
            //await Navigation.PushAsync(new CategoriesPageUser(deportistaa));
            //await Navigation.PushAsync(new AddExercise(deportistaa));
        }
        private async void Ejerciciopersonalizado_Clicked(object sender, EventArgs e)
        {
            await App.Navigator.PushAsync(new ExercisePersonal(deportistaa));
            // MainViewModel.GetInstance().Categoriess = new CategoriesViewModelUser(deportistaa);
            //Application.Current.MainPage = new NavigationPage(new LoginPage());
            //  await App.Navigator.PushAsync(new CategoriesPageUser());
            // await Navigation.PushAsync(new ExercisePersonal(deportistaa));
            //MainViewModel.GetInstance().Categoriess = new CategoriesViewModelUser(deportistaa);
            //Application.Current.MainPage = new NavigationPage(new LoginPage());
            //await App.Navigator.PushAsync(new CategoriesPageUser());
            //await Navigation.PushAsync(new CategoriesPageUser(deportistaa));
            //await Navigation.PushAsync(new AddExercise(deportistaa));
        }
    
    }
}
using Kapta.Categorias;
using Kapta.Deportistas;
using Kapta.Herramientas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kapta.Ejercicios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableExercisesView : ContentPage
    {
        Deportista deportistaa;
        public TableExercisesView(Deportista deportista)
        {
            InitializeComponent();
            this.deportistaa = deportista;

            //listaListView.RowHeight = 70;

            this.Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10, 10, 10, 10),
            new Thickness(10, 10, 10, 10));


            //labelTabla.Text = "Tabla de ejercicios " + Environment.NewLine + deportistaa.NombreCompleto;
            labelTabla.Text = deportistaa.NombreCompleto;
            //listaListView.ItemTemplate = new DataTemplate(typeof(TablaEjerciciosCell));
            listaListView.RowHeight = 70;
            //mostrar la lista con los datos previamente ingresados



            nuevo.Clicked += Nuevo_Clicked;
            enviaremail.Clicked += Enviaremail_Clicked;
            listaListView.ItemSelected += ListaListView_ItemSelected;
            //  listaListView.ItemSelected += ListaListView_ItemSelected;
        }

        private async void Enviaremail_Clicked(object sender, EventArgs e)
        {
            int vacio = 0;
            int numejercicio = 1;
            string datitos = "";
            using (var datos = new DataAccess())
            {
                var datoss = datos.GetEjercicioDeportista(this.deportistaa.IDDeportista);
                foreach (var s in datoss)
                {
                    vacio = 1;
                    datitos = datitos + "*Ejercicio " + numejercicio + "*" + Environment.NewLine + s.Nombreejercicio + Environment.NewLine + "*Descripción*" + Environment.NewLine + s.Descripcion + Environment.NewLine;
                    if (s.ComentarioEjercicio != null)
                    {
                        datitos = datitos + "*Comentario personal*" + Environment.NewLine + s.ComentarioEjercicio + Environment.NewLine;
                    }
                    datitos = datitos + Environment.NewLine;
                    numejercicio++;
                }
            }

            if (vacio == 0)
            {
                await DisplayAlert("Alerta", "Debe añadir algún ejercicio para poder enviarlos", "Aceptar");
            }
            else
            {


                var fromAddress = new MailAddress("pablogf21096@gmail.com", "KAPTA");
                var toAddress = new MailAddress(deportistaa.Email, deportistaa.NombreCompleto);
                const string fromPassword = "Pasesionthebest";
                const string subject = "EQUIPO KAPTA";
                //string body = datitos;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = datitos,
                })

                {
                    smtp.Send(message);
                    await DisplayAlert("Confirmación", "Ejercicios enviados", "Aceptar");
                }

            }


        }

        private async void Nuevo_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Categoriess = new CategoriesViewModelUser(deportistaa);
            //Application.Current.MainPage = new NavigationPage(new LoginPage());
            await App.Navigator.PushAsync(new CategoriesPageUser());
            //await Navigation.PushAsync(new CategoriesPageUser(deportistaa));
            //await Navigation.PushAsync(new AddExercise(deportistaa));
        }

        //actualizala lista en la pantalla
        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var datos = new DataAccess())
            {
                listaListView.ItemsSource = datos.GetEjercicioDeportista(this.deportistaa.IDDeportista);
            }
        }

        private async void ListaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditExercise((TablaEjercicios)e.SelectedItem));
        }


    }
}
using Kapta.Deportistas;
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
    public partial class SelectMemberInf : ContentPage
    {
        List<Lugar> opciones;
        List<string> tipos;
        Deportista deportistaa;
        int clavedeportistaa;
        public SelectMemberInf(Deportista deportista)
        {
            InitializeComponent();
            deportistaa = deportista;
            opciones = new List<Lugar>();
            tipos = new List<string>();
            // clavedeportistaa = iDDeportista;

            opciones.Add(new Lugar { NombreLugar = "Cadera" });
            opciones.Add(new Lugar { NombreLugar = "Muslo parte anterior" });
            opciones.Add(new Lugar { NombreLugar = "Muslo posterior" });
            opciones.Add(new Lugar { NombreLugar = "Pantorrilla" });
            opciones.Add(new Lugar { NombreLugar = "Rodilla" });
            opciones.Add(new Lugar { NombreLugar = "Tobillo" });
            /*
            tipos.Add("Muscular");
            tipos.Add("Ligamentosa");
            tipos.Add("Tendinosa");
            */
            foreach (var opcion in opciones)
            {
                pickerlugar.Items.Add(opcion.NombreLugar);
            }

            //tipos.Add("Muscular");
            tipos.Add("Ligamentosa");
            tipos.Add("Tendinosa");

            /*
            foreach (var tipo in tipos)
            {
                pickertipo.Items.Add(tipo);
            }*/

            Agregarlesion.Clicked += Agregarlesion_Clicked;
        }
        private void eventopickerlugar(object sender, EventArgs e)
        {
            if (pickerlugar.SelectedItem.ToString() == "Rodilla" || pickerlugar.SelectedItem.ToString() == "Tobillo")
            {
                foreach (var tipo in tipos)
                {
                    pickertipo.Items.Remove(tipo);
                }
                pickertipo.Items.Remove("Muscular");
                foreach (var tipo in tipos)
                {
                    pickertipo.Items.Add(tipo);
                }
            }
            else
            {
                // pickerlugar.Items.Remove("Muscular");
                foreach (var tipo in tipos)
                {
                    pickertipo.Items.Remove(tipo);
                }
                //tipos.Add("Muscular");

                pickertipo.Items.Remove("Muscular");
                pickertipo.Items.Add("Muscular");
                foreach (var tipo in tipos)
                {
                    pickertipo.Items.Add(tipo);
                }

            }
        }

        private async void Agregarlesion_Clicked(object sender, EventArgs e)
        {
            if (pickerlugar.SelectedItem == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Debe seleccionar el lugar de la lesión",
                   "Aceptar");
                return;
            }
            if (pickertipo.SelectedItem == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Debe seleccionar el tipo de lesión",
                   "Aceptar");
                return;
            }
            if(string.IsNullOrEmpty(this.numeroLesionesEntry.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   "Debe seleccionar el número de lesiones",
                   "Aceptar");
                return;
            }
            //creamos la lesion
            var lesion = new Lesion
            {
                clavedeportista = deportistaa.IDDeportista,
                Lugar = pickerlugar.SelectedItem.ToString(),
                Tipo = pickertipo.SelectedItem.ToString(),
                NumLesiones = int.Parse(numeroLesionesEntry.Text),



            };


            //insertamos el deportista en la base de datos
            using (var datos = new DataAccess())
            {
                datos.InsertLesion(lesion);
                //listaListView.ItemsSource = datos.GetDeportistas();
            }
            //await DisplayAlert("Confirmación", "Lesion agregada", "Aceptar");

            DependencyService.Get<IMessage>().LongAlert("Lesión añadida");
            //new NavigationPage(new Trabajo.Paginas.Inicio());
            // await Navigation.PushAsync(new Trabajo.LesionesView(deportistaa));
            // await Navigation.PopAsync();
            // new NavigationPage(new LesionesView(deportistaa));
            //await Navigation.PushAsync(new LesionesView(deportistaa));

            PopUntilDestination(typeof(LesionesView));
        }

        public class Lugar
        {
            public string NombreLugar
            {
                get;
                set;
            }
        }
        void PopUntilDestination(Type DestinationPage)
        {
            int LeastFoundIndex = 0;

            int PagesToRemove = 0;

            for (int index = Navigation.NavigationStack.Count - 2; index > 0; index--)
            {
                if (Navigation.NavigationStack[index].GetType().Equals(DestinationPage))
                {
                    break;
                }
                else
                {
                    LeastFoundIndex = index;
                    PagesToRemove++;
                }
            }

            for (int index = 0; index < PagesToRemove; index++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[LeastFoundIndex]);
            }

            Navigation.PopAsync();
        }
    }
}
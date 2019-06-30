
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
    public partial class SelectMemberSup : ContentPage
    {
        List<Lugar> opciones;
        List<string> tipos;
        //int clavedeportistaa;
        Deportista deportistaa;
        public SelectMemberSup(Deportista deportista)
        {
            InitializeComponent();
            deportistaa = deportista;
            opciones = new List<Lugar>();
            tipos = new List<string>();
            //clavedeportistaa = iDDeportista;

            opciones.Add(new Lugar { NombreLugar = "Flexores de codo" });
            opciones.Add(new Lugar { NombreLugar = "Extensores de codo" });
            opciones.Add(new Lugar { NombreLugar = "Hombro" });
            opciones.Add(new Lugar { NombreLugar = "Muñeca" });

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
            tipos.Add("Luxación");
            tipos.Add("Fractura");

            /*
            foreach (var tipo in tipos)
            {
                pickertipo.Items.Add(tipo);
            }*/

            Agregarlesion.Clicked += Agregarlesion_Clicked;
        }
        private void eventopickerlugar(object sender, EventArgs e)
        {
            if (pickerlugar.SelectedItem.ToString() == "Hombro" || pickerlugar.SelectedItem.ToString() == "Muñeca")
            {
                foreach (var tipo in tipos)
                {
                    pickertipo.Items.Remove(tipo);
                }
                pickertipo.Items.Remove("Elongación");
                pickertipo.Items.Remove("Desgarro");

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

                pickertipo.Items.Remove("Elongación");
                pickertipo.Items.Remove("Desgarro");

                pickertipo.Items.Add("Elongación");
                pickertipo.Items.Add("Desgarro");


            }
        }

        private async void Agregarlesion_Clicked(object sender, EventArgs e)
        {
            //creamos la lesion
            var lesion = new Lesion
            {
                //clavedeportista = clavedeportistaa,
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
            DependencyService.Get<IMessage>().LongAlert("Lesión añadida");

            //await DisplayAlert("Confirmación", "Lesion agregada", "Aceptar");
            //new NavigationPage(new Trabajo.Paginas.Inicio());
            // Navigation.RemovePage(new SeleccionarLesionMS(deportistaa));

            //esta funcion hace que navegues a la página deseada
            PopUntilDestination(typeof(LesionesView));

            //new NavigationPage(new LesionesView(deportistaa));
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
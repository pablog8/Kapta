using Kapta.Deportistas;
using Sales.Lesiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using XLabs.Forms.Controls;

namespace Kapta.Lesiones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectMemberView : ContentPage
    {
        //int clave;
        Deportista deportistaa;
        int flag;
        string[] miembros = { "Miembro superior", "Miembro inferior" };
        //string[] imagenes = { "miembrosuperior.jgp", "miembroinferior.jpg" };
        public SelectMemberView(Deportista deportista)
        {
            deportistaa = deportista;
            InitializeComponent();
            Inicializar();
        }

        private void Inicializar()
        {
            pickermiembro.ItemsSource = miembros;
            //botonmiembro.CheckedChanged += Botonmiembro_CheckedChanged;
        }
        private void eventopickermiembro(object sender, EventArgs e)
        {
            if (pickermiembro.SelectedItem.ToString() == "Miembro superior")
            {
                imgmiembro.Source = "miembrosuperior.jpg";
                botonsiguiente.IsVisible = true;
                flag = 0;
            }
            else
            {
                imgmiembro.Source = "miembroinferior.jpg";
                botonsiguiente.IsVisible = true;
                flag = 1;
            }
        }
        public void EventoBoton(Object sender, EventArgs e)
        {
            if (flag == 0)
                ((NavigationPage)this.Parent).PushAsync(new SelectMemberSup(deportistaa));
            if (flag == 1)
                ((NavigationPage)this.Parent).PushAsync(new SelectMemberInf(deportistaa));

        }

        /*
                private void Botonmiembro_CheckedChanged(object sender, int e)
                {
                    var boton = sender as CustomRadioButton;

                    if(boton==null || boton.Id == -1)
                    {
                        return;
                    }
                    if (boton.Id == 0) {
                        imgmiembro.Source = "miembrosuperior.jpg";
                        //botonsiguiente.IsEnabled = true;
                        botonsiguiente.IsVisible = true;
                        flag = 0;

                    }
                    if(boton.Id==1)
                    {
                        imgmiembro.Source = "miembroinferior.jpg";
                        //botonsiguiente.IsEnabled = true;
                        botonsiguiente.IsVisible = true;
                        flag = 1;

                    }

                }
                public void EventoBoton(Object sender, EventArgs e)
                {
                    if(flag==0)
                    ((NavigationPage)this.Parent).PushAsync(new SeleccionarLesionMS(deportistaa));
                    if (flag==1)
                    ((NavigationPage)this.Parent).PushAsync(new SeleccionarLesion(deportistaa));

                }*/
    }
}
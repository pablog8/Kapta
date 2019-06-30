namespace Kapta
{
    using GalaSoft.MvvmLight.Command;
    using Kapta.Categorias;
    using Kapta.Contacto;
    using Kapta.Deportistas;
    using Kapta.Ejercicios;
    using Kapta.Herramientas.Helpers;
    using Kapta.Usuarios;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Commands
        
        public ICommand GotoCommand
        {
            get
            {
                return new RelayCommand(Goto);
            }
        }
        
        private async void Goto()
        {
            if (this.PageName == "LoginPage")
            {

                Settings.AccessToken = string.Empty;
                Settings.TokenType = string.Empty;
                Settings.IsRemembered = false;

                MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }/*
            else if (this.PageName =="AboutPage")
            {
                //para que oculte la master page
                App.Master.IsPresented = false;
                await App.Navigator.PushAsync(new MapPage());
            }*/
        /*
        else if (this.PageName == "Presentation")
        {
            //para que oculte la master page
            App.Master.IsPresented = false;
            await App.Navigator.PushAsync(new Presentacion());
        }
        */
        else if (this.PageName == "Contact")
        {
            //para que oculte la master page
            App.Master.IsPresented = false;
            MainViewModel.GetInstance().Contact = new ContactViewModel();
            //Application.Current.MainPage = new NavigationPage(new Contact());
            await App.Navigator.PushAsync(new Contact());

        }
            
        else if (this.PageName == "Paciente")
        {
            //para que oculte la master page
            App.Master.IsPresented = false;
            //MainViewModel.GetInstance().Contact = new ContactViewModel();
            //Application.Current.MainPage = new NavigationPage(new Contact());

            await App.Navigator.PushAsync(new AthletePage(MainViewModel.GetInstance().UserASP.Email));

        }
        else if (this.PageName == "Exercises")
        {
            //para que oculte la master page
            App.Master.IsPresented = false;
            //MainViewModel.GetInstance().Contact = new ContactViewModel();
            //Application.Current.MainPage = new NavigationPage(new Contact());
            MainViewModel.GetInstance().Categories = new CategoriesViewModel();
            //Application.Current.MainPage = new NavigationPage(new LoginPage());
            await App.Navigator.PushAsync(new CategoriesPage());

        }
        else if (this.PageName == "Videos")
        {
            //para que oculte la master page
            App.Master.IsPresented = false;
            //MainViewModel.GetInstance().Contact = new ContactViewModel();
            //Application.Current.MainPage = new NavigationPage(new Contact());
            MainViewModel.GetInstance().Videos = new VideosViewModel();
            //Application.Current.MainPage = new NavigationPage(new LoginPage());
            await App.Navigator.PushAsync(new VideosPage());

        }
        
        else if (this.PageName == "Financiacion")
        {
            //para que oculte la master page
            App.Master.IsPresented = false;
            //MainViewModel.GetInstance().Contact = new ContactViewModel();
            //Application.Current.MainPage = new NavigationPage(new Contact());
            //MainViewModel.GetInstance().Videos = new VideosViewModel();
            //Application.Current.MainPage = new NavigationPage(new LoginPage());
            await App.Navigator.PushAsync(new Financiacion());

        }
        
    }
    
        #endregion

    }
}

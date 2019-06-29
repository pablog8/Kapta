using Kapta.Ejercicios;
using Kapta.Herramientas.Helpers;
using Kapta.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kapta
{
	public partial class App : Application
	{
        public static NavigationPage Navigator { get; internal set; }

        public App ()
		{
			InitializeComponent();

            if (Settings.IsRemembered && !string.IsNullOrEmpty(Settings.AccessToken))
            {

                MainViewModel.GetInstance().Exercises = new ExercisesViewModel();
                MainPage = new MasterPage(); 
            }



            else
            {
                MainViewModel.GetInstance().Login = new LoginViewModel();
                MainPage = new NavigationPage(new LoginPage());
            }

           // MainViewModel.GetInstance().Login = new LoginViewModel();
            //MainPage = new NavigationPage (new LoginPage());
            //MainPage = new NavigationPage(new ExercisesPage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

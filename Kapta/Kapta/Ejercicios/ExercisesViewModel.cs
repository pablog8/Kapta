using GalaSoft.MvvmLight.Command;
using Kapta.Common.Models;
using Kapta.Herramientas.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kapta.Ejercicios
{
    public class ExercisesViewModel : BaseViewModel
    {
        private APIService apiService;
        private bool isRefreshing;
        private ObservableCollection<Exercise> exercises;

        public ObservableCollection<Exercise> Exercises
        {
            get { return this.exercises; }
            set { this.SetValue(ref this.exercises, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ExercisesViewModel()
        {
            this.apiService = new APIService();
            this.LoadExercises();
        }

        //Cargar ejercicios
        private async void LoadExercises()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                return;

            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetList<Exercise>(url, "/api", "/Exercises");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var list = (List<Exercise>)response.Result;
            this.Exercises = new ObservableCollection<Exercise>(list);
            this.IsRefreshing = false;
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadExercises);
            }
        }
    }
}

using GalaSoft.MvvmLight.Command;
using Kapta.Common.Models;
using Kapta.Herramientas.Helpers;
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
        #region Attributes
        private APIService apiService;
        private bool isRefreshing;
        #endregion

        #region Properties
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
        #endregion

        #region Constructors
        public ExercisesViewModel()
        {
            instance = this;
            this.apiService = new APIService();
            this.LoadExercises();
        }
        #endregion

        #region Singleton
        private static ExercisesViewModel instance;

        public static ExercisesViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ExercisesViewModel();
            }
            return instance;
        }

        #endregion
        //Cargar ejercicios
        #region Methods
        private async void LoadExercises()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlExercisesController"].ToString();

            var response = await this.apiService.GetList<Exercise>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var list = (List<Exercise>)response.Result;
            this.Exercises = new ObservableCollection<Exercise>(list);
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadExercises);
            }
        }
        #endregion

    }
}

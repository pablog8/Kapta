using Kapta.Common.Models;
using Kapta.Herramientas.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Kapta.Ejercicios
{
    public class ExercisesViewModel : BaseViewModel
    {
        private APIService apiService;

        private ObservableCollection<Exercise> exercises;

        public ObservableCollection<Exercise> Exercises
        {
            get { return this.exercises; }
            set { this.SetValue(ref this.exercises, value); }
        }

        public ExercisesViewModel()
        {
            this.apiService = new APIService();
            this.LoadExercises();
        }

        private async void LoadExercises()
        {
            var response = await this.apiService.GetList<Exercise>("http://kaptaapi.azurewebsites.net", "/api", "Exercises");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var list = (List<Exercise>)response.Result;
            this.Exercises = new ObservableCollection<Exercise>(list);
        }
    }
}

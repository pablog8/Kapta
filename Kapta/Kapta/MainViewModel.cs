namespace Kapta
{
    using GalaSoft.MvvmLight.Command;
    using Kapta.Ejercicios;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainViewModel
    {
        public ExercisesViewModel Exercises { get; set; }

        public AddExerciseViewModel AddExercise { get; set; }


        public MainViewModel()
        {
            this.Exercises = new ExercisesViewModel();
        }

        #region Commands
        //cuando toque en el icono +, lanza el comando AddProductComand, que devuelve la pagina del metodo GoToAddProduct
        public ICommand AddExerciseCommand
        {
            get
            {
                return new RelayCommand(GoToAddExercise);
            }
        }
        
        private async void GoToAddExercise()
        {
            //Antes de lanzar la pagina instanciamos la viewmodel que gobierna la pagina
            this.AddExercise = new AddExerciseViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddExercisePage());
            //await ApnewExercise.Navigator.PushAsync(new AddExercisePage());
        }
        
        #endregion
    }
}

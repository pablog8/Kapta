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
        #region Properties
        public EditExerciseViewModel EditExercise { get; set; }

        public ExercisesViewModel Exercises { get; set; }

        public AddExerciseViewModel AddExercise { get; set; }
        #endregion


        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Exercises = new ExercisesViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion
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

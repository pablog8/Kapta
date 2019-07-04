using System;
using System.Collections.Generic;
using System.Text;

namespace Kapta.Categorias
{
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Kapta.Ejercicios;

    //hereda de category para mantener el modelo category puro sin métodos
    public class CategoryItemViewModel : Category
    {
        #region Commands
        public ICommand GotoCategoryCommand
        {
            get
            {
                return new RelayCommand(GotoCategory);
            }
        }

        private async void GotoCategory()
        {
            if (MainViewModel.GetInstance().UserASP.Email == "pablo.kapta@gmail.com")
            {
                MainViewModel.GetInstance().Exercises= new ExercisesViewModel(this);// (this);
                await App.Navigator.PushAsync(new ExercisesPage());
            }
            else
            {
                MainViewModel.GetInstance().Exercises = new ExercisesViewModel(this);// (this);
                await App.Navigator.PushAsync(new ExercisesPageUser());
            }
            /*
            MainViewModel.GetInstance().Exercises = new ExercisesViewModel(this);// (this);
                await App.Navigator.PushAsync(new ExercisesPage());
           */
        }
        #endregion
    }

}

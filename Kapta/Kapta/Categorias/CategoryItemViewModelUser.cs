using System;
using System.Collections.Generic;
using System.Text;

namespace Kapta.Categorias
{
    using System.Windows.Input;

    using GalaSoft.MvvmLight.Command;
    using Kapta;
    using Kapta.Common.Models;
    using Kapta.Deportistas;
    using Kapta.Ejercicios;
    using Sales.Lesiones;


    //hereda de category para mantener el modelo category puro sin métodos
    public class CategoryItemViewModelUser : Category
    {
        Deportista deportistaa;
        public CategoryItemViewModelUser(Deportista deportista)
        {
            //this.apiService = new APIService();
            this.deportistaa = deportista;
            //this.LoadCategories();
        }


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
            MainViewModel.GetInstance().Exercisess = new ExercisesViewModelUser(this, this.deportistaa);// (this);
                                                                                                        // MainViewModel.GetInstance().Products = new ProductsViewModel(this);// (this);
            await App.Navigator.PushAsync(new ExercisesUserExercise());
            /*
            if (MainViewModel.GetInstance().UserASP.Email == "pablo@gmail.com")
            {
              
            }
            else
            {
                MainViewModel.GetInstance().Exercises = new ExercisesViewModel(this);// (this);
                await App.Navigator.PushAsync(new ExercisesPageUser());
            }
            */



        }
        #endregion
    }

}
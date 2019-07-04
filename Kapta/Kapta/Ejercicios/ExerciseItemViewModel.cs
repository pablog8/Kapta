namespace Kapta.Ejercicios
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GalaSoft.MvvmLight.Command;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Kapta.Common.Models;
    using Kapta.Herramientas.Services;
    using Kapta.Herramientas.Helpers;

    public class ExerciseItemViewModel : Exercise
    {

        #region Attributes
        private APIService apiService;
        #endregion

        #region Constructors
        public ExerciseItemViewModel()
        {
            this.apiService = new APIService();
        }

        #endregion


        #region Commands
        
        public ICommand EditExerciseCommand
        {
            get
            {
                return new RelayCommand(EditExercise);
            }
        }

        private async void EditExercise()
        {
            /*
            MainViewModel.GetInstance().EditExercise = new EditExerciseViewModel(this);
            await App.Navigator.PushAsync(new EditExercisePage());*/
            if (MainViewModel.GetInstance().UserASP.Email == "pablo@gmail.com")
            {
                //Creamos una instancia y ligarlo a la viewmodel
                MainViewModel.GetInstance().EditExercise = new EditExerciseViewModel(this);
                await App.Navigator.PushAsync(new EditExercisePage());
            }
            else
            {
                //Creamos una instancia y ligarlo a la viewmodel
                MainViewModel.GetInstance().EditExercise = new EditExerciseViewModel(this);

                //tiene que apilar otra pagina
                await App.Navigator.PushAsync(new EditExerciseUser());
            }
            /*
            if (MainViewModel.GetInstance().UserASP.Email == "prueba3@usal.es")
            {
                //Creamos una instancia y ligarlo a la viewmodel
                MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);
                await App.Navigator.PushAsync(new EditProductPage());
            }
            else
            {
            
                //Creamos una instancia y ligarlo a la viewmodel
                MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);

                //tiene que apilar otra pagina
                await App.Navigator.PushAsync(new EditProductUser());
           // }
           */

        }
        
        public ICommand DeleteExerciseCommand
        {
            get
            {
                return new RelayCommand(DeleteExercise);
            }
        }

        private async void DeleteExercise()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.DeleteConfirmation,
                Languages.Yes,
                Languages.No);
            if (!answer)
            {
                return;
            }
            //eliminamos el producto
            //comprobamos si hay conexión
            var connection = await this.apiService.CheckConnection();

            //si la conexion a internet no ha sido exitosa
            if (!connection.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }

            //vamos a la api
            var url = Application.Current.Resources["UrlAPI"].ToString();

            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            var controller = Application.Current.Resources["UrlExercisesController"].ToString();

            var response = await this.apiService.Delete(url, prefix, controller, this.ExerciseId, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);

                return;
            }

            //hay que actualizar la lista, llamamos a  SINGLETON
            var exercisesViewModel = ExercisesViewModel.GetInstance();

            //buscamos el producto en la lista y lo eliminamos
            var deletedExercise = exercisesViewModel.MyExercises.Where(p => p.ExerciseId == this.ExerciseId).FirstOrDefault();

            //si encontramos el producto
            if (deletedExercise != null)
            {
                exercisesViewModel.MyExercises.Remove(deletedExercise);
            }
            exercisesViewModel.RefreshList();
        }

        #endregion
    }
}

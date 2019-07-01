namespace Kapta.Ejercicios
{
    using GalaSoft.MvvmLight.Command;
    using Kapta.Common.Models;
    using Kapta.Deportistas;
    using Kapta.Herramientas.Helpers;
    using Kapta.Herramientas.Services;
    using Sales.Lesiones;

    using System;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ExerciseItemViewModelUser : Exercise
    {

        #region Attributes
        private APIService apiService;
        #endregion

        #region Constructors
        Deportista deportistaa;
        public ExerciseItemViewModelUser(Deportista deportista)
        {
            this.deportistaa = deportista;
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

            if (MainViewModel.GetInstance().UserASP.Email == "pablo@gmail.com")
            {
                //Creamos una instancia y ligarlo a la viewmodel
                MainViewModel.GetInstance().EditExercisee = new EditExerciseMessageViewModel(this, deportistaa);
                await App.Navigator.PushAsync(new EditProductExercise());
            }
            else
            {
                //Creamos una instancia y ligarlo a la viewmodel
                MainViewModel.GetInstance().EditExercise = new EditExerciseViewModel(this);

                //tiene que apilar otra pagina
                await App.Navigator.PushAsync(new EditExerciseUser());
            }




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

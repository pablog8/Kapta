using GalaSoft.MvvmLight.Command;
using Kapta.Common.Models;
using Kapta.Herramientas.Helpers;
using Kapta.Herramientas.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kapta.Ejercicios
{
    public class EditExerciseViewModel : BaseViewModel
    {
        #region Atributes
        private Exercise exercise;
        private MediaFile file;
        private ImageSource imageSource;
        private APIService apiService;
        private bool isRunning;
        private bool isEnabled;
       
        #endregion

        #region Properties
        public Exercise Exercise
        {
            get { return this.exercise; }
            set { this.SetValue(ref this.exercise, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        #endregion

        #region Constructors
        public EditExerciseViewModel(Exercise exercise)
        {
            this.exercise = exercise;
            this.apiService = new APIService();
            this.IsEnabled = true;
            this.ImageSource = exercise.ImageFullPath;
            
        }
        #endregion

        #region Commands

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }



        private async void ChangeImage()
        {
            //inicializamos librería de fotos
            await CrossMedia.Current.Initialize();

            //preguntamos de donde se quiere obtener la imagen.
            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            //cuando pulsamos cancelar
            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            //si tomamos la foto con la cámara
            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }

            //si el usuario quiere la foto de la galería
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            //si el usuario si ha seleccionado una imagen ( de galería o de la cámara)
            //Capturamos la imagen
            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Exercise.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Exercise.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DescriptionError,
                    Languages.Accept);
                return;
            }
            /*
            if (string.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PriceError,
                    Languages.Accept);
                return;
            }
            var price = decimal.Parse(this.Price);
            /*
            if(price < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PriceError,
                    Languages.Accept);
                return;
            }*/
            /*
            if (this.Category == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CategoryError,
                    Languages.Accept);
                return;
            }
            */
            this.IsRunning = true;
            this.IsEnabled = false;

            //chekea la conexion
            var connection = await this.apiService.CheckConnection();
            //si la conexion a internet no ha sido exitosa
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }

            //para saber si se cogió o no foto

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
                this.Exercise.ImageArray = imageArray;
            }

            // var location = await this.GetLocation();

            var url = Application.Current.Resources["UrlAPI"].ToString();

            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            var controller = Application.Current.Resources["UrlExercisesController"].ToString();

            //invocamos el metodo put del apiservice
            var response = await this.apiService.Put(url, prefix, controller, this.Exercise, this.Exercise.ExerciseId);

            //preguntamos si lo grabó de manera exitosa
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var newExercise = (Exercise)response.Result;

            //adicionamos el producto a la colección
            var exercisesViewModel = ExercisesViewModel.GetInstance();

            //buscamos ejercicio lo eliminamos y lo añadimos
            var oldExercise = exercisesViewModel.MyExercises.Where(p => p.ExerciseId == this.Exercise.ExerciseId).FirstOrDefault();
            if (oldExercise != null)
            {
                exercisesViewModel.MyExercises.Remove(oldExercise);
            }

            exercisesViewModel.MyExercises.Add(newExercise);
            exercisesViewModel.RefreshList();
            // la ordenamos
            //viewModel.Exercises = viewModel.Exercises.OrderBy(p => p.Description).ToList();


            //si lo hizo de manera exitosa hacemos el back
            this.IsRunning = false;
            this.IsEnabled = true;
            //Desapilamos
            await Application.Current.MainPage.Navigation.PopAsync();
            // await App.Navigator.PopAsync();

        }
        /*
        private async Task<Position> GetLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var location = await locator.GetPositionAsync();
            return location;
        }
        */
        #endregion
    }
}

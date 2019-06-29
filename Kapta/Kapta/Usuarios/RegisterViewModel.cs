namespace Kapta.Usuarios
{
    using GalaSoft.MvvmLight.Command;
    using Kapta.Herramientas.Helpers;
    using Kapta.Herramientas.Services;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class RegisterViewModel : BaseViewModel
    {

        #region Attributes
        //para fotos
        private MediaFile file;

        private ImageSource imageSource;

        private APIService apiService;

        private bool isRunning;

        private bool isEnabled;

        #endregion

        #region Properties
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

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
        public RegisterViewModel()
        {
            this.apiService = new APIService();
            //inicializamos el boton
            this.IsEnabled = true;
            this.ImageSource = "nouser";

        }
        #endregion

        #region Commands

        //para guardar el usuario
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.FirstNameError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LastNameError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.EMail))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EMailError,
                    Languages.Accept);
                return;
            }
            /*
            if (!RegexHelper.IsValidEmailAddress(this.EMail))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EMailError,
                    Languages.Accept);
                return;
            }
            */
            if (string.IsNullOrEmpty(this.Phone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordError,
                    Languages.Accept);
                return;
            }

            if (this.Password.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.PasswordConfirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordConfirmError,
                    Languages.Accept);
                return;
            }


            if (!this.Password.Equals(this.PasswordConfirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordsNoMatch,
                    Languages.Accept);
                return;
            }

            //valida conexión a internet
            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }

            //verificamos si el usuario cargó una foto o no
            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }
            /*
            var userRequest = new UserRequest
            {
                Address = this.Address,
                EMail = this.EMail,
                FirstName = this.FirstName,
                ImageArray = imageArray,
                LastName = this.LastName,
                Password = this.Password,
            };
            */
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsersController"].ToString();
            //Lo mandamos sin token porque el usuario todavia no se ha logueado
            //var response = await this.apiService.Post(url, prefix, controller, userRequest);
            /*
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }
            */
            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.RegisterConfirmation,
                Languages.Accept);

            await Application.Current.MainPage.Navigation.PopAsync();


        }

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
                        Name = "nouser.jpg",
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

        #endregion



    }
}

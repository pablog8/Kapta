namespace Kapta.Ejercicios
{
    using GalaSoft.MvvmLight.Command;
    using Kapta.Herramientas.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AddExerciseViewModel : BaseViewModel
    {
        #region Attributes

        //private MediaFile file;
        //private ImageSource imageSource;
        //private APIService apiService;
        private bool isRunning;
        private bool isEnabled;
        //private ObservableCollection<Category> categories;
        //private Category category;
        #endregion

        #region Properties

        //public List<Category> MyCategories { get; set; }

        //cuando seleccionamos una categoria el selected item
        /*
        public Category Category
        {

            get { return this.category; }
            set { this.SetValue(ref this.category, value); }

        }

        //la que bindamos al itemsource
        public ObservableCollection<Category> Categories
        {
            get { return this.categories; }
            set { this.SetValue(ref this.categories, value); }
        }
        */
        public string Name { get; set; }

        public string Description { get; set; }

        //public string Price { get; set; }

        //public string Remarks { get; set; }

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
        /*
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        */
        #endregion

        #region Constructors
            
        public AddExerciseViewModel()
        {
            //this.apiService = new APIService();
            this.IsEnabled = true;
            //this.ImageSource = "noexercise";
            //this.LoadCategories();
        }
        
        #endregion

        #region Methods
            /*
        private async void LoadCategories()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            //var connection = await this.apiService.CheckConnection();
           // if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                //await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }
            /*
            var answer = await this.LoadCategoriesFromAPI();
            if (answer)
            {
                this.RefreshList();

            }

            this.IsRunning = false;
            this.IsEnabled = true;
        }

        private void RefreshList()
        {
            //this.Categories = new ObservableCollection<Category>(this.MyCategories.OrderBy(c => c.Description));
        }
        /*
        private async Task<bool> LoadCategoriesFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlCategoriesController"].ToString();
            var response = await this.apiService.GetList<Category>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }

            this.MyCategories = (List<Category>)response.Result;
            return true;
        }
        */
        #endregion


        #region Commands
            /*
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }
        */

        /*
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
        */
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }
        
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Description))
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
            }

            // var location = await this.GetLocation();

            //metemos lo que mandemos al post como un producto
            /*
            var product = new Product
            {
                Description = this.Description,
                //Price = price,
                Remarks = this.Remarks,
                ImageArray = imageArray,
                CategoryId = this.Category.CategoryId,
                UserId = MainViewModel.GetInstance().UserASP.Id,
                //    Latitude = location == null ? 0 : location.Latitude,
                //    Longitude = location == null ? 0 : location.Longitude,

            };


            var url = Application.Current.Resources["UrlAPI"].ToString();

            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            //invocamos el metodo post del apiservice
            var response = await this.apiService.Post(url, prefix, controller, product, Settings.TokenType, Settings.AccessToken);

            //preguntamos si lo grabó de manera exitosa
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var newProduct = (Product)response.Result;

            //adicionamos el producto a la colección
            var productsViewModel = ProductsViewModel.GetInstance();
            productsViewModel.MyProducts.Add(newProduct);
            productsViewModel.RefreshList();
            // la ordenamos
            //viewModel.Products = viewModel.Products.OrderBy(p => p.Description).ToList();


            //si lo hizo de manera exitosa hacemos el back
            this.IsRunning = false;
            this.IsEnabled = true;
            //Desapilamos
            await App.Navigator.PopAsync();

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
}

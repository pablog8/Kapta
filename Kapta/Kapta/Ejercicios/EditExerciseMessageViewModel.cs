namespace Kapta.Ejercicios
{
    using GalaSoft.MvvmLight.Command;
    using Kapta.Common.Models;
    using Kapta.Deportistas;
    using Kapta.Herramientas.Helpers;
    using Kapta.Herramientas.Services;
    using Plugin.Media;
    using Plugin.Media.Abstractions;

    using Sales.Lesiones;

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditExerciseMessageViewModel : BaseViewModel
    {
        #region Atributes

        private Exercise exercise;
        private MediaFile file;
        private ImageSource imageSource;
        private APIService apiService;
        private bool isRunning;
        private bool isEnabled;

        private ObservableCollection<Category> categories;

        //cuando ya seleccionamos la categoría
        private Category category;

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

        //lo que recibimos del servicio
        public List<Category> MyCategories { get; set; }

        //lo que depende del atributo category

        public Category Category
        {
            get { return this.category; }
            set { this.SetValue(ref this.category, value); }
        }

        //la que cargamos del servicio
        public ObservableCollection<Category> Categories
        {
            get { return this.categories; }
            set { this.SetValue(ref this.categories, value); }
        }

        #endregion
        #region Constructors
        Deportista deportistaa;
        public EditExerciseMessageViewModel(Exercise exercise, Deportista deportista)
        {
            this.deportistaa = deportista;
            this.exercise = exercise;
            this.apiService = new APIService();
            this.IsEnabled = true;
            this.ImageSource = exercise.ImageFullPath;
            this.LoadCategories();
        }
        #endregion

        #region Commands
        /*
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }

        }

        private async void Delete()
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

            this.IsRunning = true;
            this.IsEnabled = false;
            //eliminamos el producto
            //comprobamos si hay conexión
            var connection = await this.apiService.CheckConnection();
            //si la conexion a internet no ha sido exitosa
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }

            //vamos a la api
            var url = Application.Current.Resources["UrlAPI"].ToString();

            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await this.apiService.Delete(url, prefix, controller, this.Product.ProductId, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);

                return;
            }

            //hay que actualizar la lista, llamamos a  SINGLETON
            var productsViewModel = ProductsViewModel.GetInstance();

            //buscamos el producto en la lista y lo eliminamos
            var deletedProduct = productsViewModel.MyProducts.Where(p => p.ProductId == this.Product.ProductId).FirstOrDefault();

            //si encontramos el producto
            if (deletedProduct != null)
            {
                productsViewModel.MyProducts.Remove(deletedProduct);
            }
            productsViewModel.RefreshList();

            this.IsRunning = false;
            this.IsEnabled = true;

            await App.Navigator.PopAsync();


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
        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand(Add);
            }
        }

        private async void Add()
        {
            /*
            if (string.IsNullOrEmpty(this.Product.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DescriptionError,
                    Languages.Accept);
                return;
            }
            */
            /*
            if (this.Product.Price < 0)
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
            /*
            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
                this.Product.ImageArray = imageArray;
            }
            */
            //por si el usuario cambia la categoría
            //this.Product.CategoryId = this.Category.CategoryId;
            string descripcionejercicio = this.exercise.Description;
            string nombreejercicio = this.exercise.Name;
            string categoriaa = this.category.Description;
            //creamos el deportista
            var ejercicio = new TablaEjercicios
            {
                Nombreejercicio = nombreejercicio,
                Descripcion = descripcionejercicio,
                clavedeportista = deportistaa.IDDeportista,

                // Salario = decimal.Parse(salarioEntry.Text),

            };

            //insertamos el deportista en la base de datos
            using (var datos = new DataAccess())
            {
                datos.InsertEjercicio(ejercicio);
                //listaListView.ItemsSource = datos.GetDeportistas();
            }

            /*
            var url = Application.Current.Resources["UrlAPI"].ToString();

            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            //invocamos el metodo post del apiservice
            var response = await this.apiService.Put(url, prefix, controller, this.Product, this.Product.ProductId, Settings.TokenType, Settings.AccessToken);
            */
            //preguntamos si lo grabó de manera exitosa
            /*
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var newProduct = (Product)response.Result;
            */
            //adicionamos el producto a la colección
            //var productsViewModel = ProductsViewModel.GetInstance();

            //buscamos el producto lo eliminamos y lo volvemos a crear
            /*
            var oldProduct = productsViewModel.MyProducts.Where(p => p.ProductId == this.Product.ProductId).FirstOrDefault();

            if (oldProduct != null)
            {
                productsViewModel.MyProducts.Remove(oldProduct);
            }


            productsViewModel.MyProducts.Add(newProduct);
            productsViewModel.RefreshList();
            */
            // la ordenamos
            //viewModel.Products = viewModel.Products.OrderBy(p => p.Description).ToList();


            //si lo hizo de manera exitosa hacemos el back
            this.IsRunning = false;
            this.IsEnabled = true;
            //Desapilamos
            await App.Navigator.PopAsync(false);
            await App.Navigator.PopAsync(false);
            await App.Navigator.PopAsync(false);
            await App.Navigator.PopAsync(false);
            //PopUntilDestination(typeof(TableExercisesView));

        }
        #endregion

        #region Methods
        private async void LoadCategories()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var answer = await this.LoadCategoriesFromAPI();
            if (answer)
            {
                this.RefreshList();
            }

            this.Category = this.MyCategories.FirstOrDefault(c => c.CategoryId == this.Exercise.CategoryId);

            this.IsRunning = false;
            this.IsEnabled = true;
        }

        private void RefreshList()
        {
            this.Categories = new ObservableCollection<Category>(this.MyCategories.OrderBy(c => c.Description));
        }

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

            //cargamos la lista de categorías
            this.MyCategories = (List<Category>)response.Result;
            return true;
        }
        #endregion

    }
}

namespace Kapta.Ejercicios
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows.Input;

    using GalaSoft.MvvmLight.Command;

    using Xamarin.Forms;
    using System.Linq;
    using System.Threading.Tasks;
    using Sales.Lesiones;
    using Kapta.Herramientas.Services;
    using Kapta.Common.Models;
    using Kapta.Deportistas;
    using Kapta.Herramientas.Helpers;

    public class ExercisesViewModelUser : BaseViewModel
    {
        #region Attributes
        private string filter;

        private APIService apiService;

        private DataService dataService;

        private bool isRefreshing;

        private ObservableCollection<ExerciseItemViewModelUser> exercises;
        #endregion

        #region Properties
        public Category Category
        {
            get;
            set;
        }

        public string Filter
        {
            get
            {
                return this.filter;
            }
            set
            {
                this.filter = value;
                this.RefreshList();
            }
        }

        public List<Exercise> MyExercises { get; set; }


        public ObservableCollection<ExerciseItemViewModelUser> Exercises
        {
            get { return this.exercises; }
            set { this.SetValue(ref this.exercises, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        /*
        public ProductsViewModel()
        {
            //primera vez que llamamos a ProductsViewModel para guardarla 
            instance = this;

            this.apiService = new APIService();
            this.dataService = new DataService();
            this.LoadProducts();
        }*/
        Deportista deportistaa;
        public ExercisesViewModelUser(Category category, Deportista deportista)
        {
            instance = this;
            this.deportistaa = deportista;
            this.apiService = new APIService();
            this.Category = category;
            this.dataService = new DataService();
            this.LoadExercises();

        }
        #endregion

        //para llamar a una clase existente sin necesitad de volver a instanciarla =>SIGLETON
        #region Singleton
        private static ExercisesViewModelUser instance;


        public static ExercisesViewModelUser GetInstance()
        {/*
            if (instance == null)
            {
                return new ProductsViewModel();
            }*/
            return instance;
        }
        #endregion

        #region Methods
        //va a la API y almacena una lista en MyProducts y ejecuta RefreshList
        /*
        private async void LoadProducts()
        {
            //carga los productos
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            //si la conexion a internet no ha sido exitosa
            if (connection.IsSuccess)
            {
                //comprobamos si cargamos los datos del servidor cuando tenemos conexion a internet, si no la cargamos de la base de datos local
                var answer = await this.LoadProductsFromAPI();
                if (answer)
                {
                    this.SaveProductsToDB();
                }
            }

            else
            {
                await this.LoadProductsFromDB();
                
            }

                if(this.MyProducts == null || this.MyProducts.Count == 0)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoProductsMessage, Languages.Accept);
                    return;
                }

            

            this.RefreshList();

           
            this.IsRefreshing = false;
        }
        */
        private async void LoadExercises()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var answer = await this.LoadExercisesFromAPI();
            if (answer)
            {
                this.RefreshList();
            }

            this.IsRefreshing = false;
        }


        private async Task LoadExercisesFromDB()
        {
            this.MyExercises = await this.dataService.GetAllExercises();
        }

        private async Task SaveExercisesToDB()
        {
            await this.dataService.DeleteAllExercises();
            this.dataService.Insert(this.MyExercises);
        }
        /*
        private async Task<bool> LoadProductsFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();

            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await this.apiService.GetList<Product>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                /*
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                
                return false;
            }
    
            this.MyProducts = (List<Product>)response.Result;

            return true;
        }
    */
        private async Task<bool> LoadExercisesFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlExercisesController"].ToString();
            var response = await this.apiService.GetList<Exercise>(url, prefix, controller, this.Category.CategoryId, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }

            this.MyExercises = (List<Exercise>)response.Result;
            return true;
        }


        public void RefreshList()
        {
            //si no hay filtro en la lupa
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myListExerciseItemViewModel = this.MyExercises.Select(p => new ExerciseItemViewModelUser(this.deportistaa)
                {
                    Name = p.Name,
                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                    //IsAvailable = p.IsAvailable,
                    //Price = p.Price,
                    PublishOn = p.PublishOn,
                    ExerciseId = p.ExerciseId,
                    //Remarks = p.Remarks,
                    CategoryId = p.CategoryId,
                    UserId = p.UserId,

                });

                this.Exercises = new ObservableCollection<ExerciseItemViewModelUser>(
                    myListExerciseItemViewModel.OrderBy(p => p.Description));
            }
            //si hay texto en la lupa
            else
            {
                var myListExerciseItemViewModel = this.MyExercises.Select(p => new ExerciseItemViewModelUser(this.deportistaa)
                {
                    Name = p.Name,
                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                  //  IsAvailable = p.IsAvailable,
                    //Price = p.Price,
                    PublishOn = p.PublishOn,
                    ExerciseId = p.ExerciseId,
                    // Remarks = p.Remarks,
                    CategoryId = p.CategoryId,
                    UserId = p.UserId,


                }).Where(p => p.Description.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Exercises = new ObservableCollection<ExerciseItemViewModelUser>(
                    myListExerciseItemViewModel.OrderBy(p => p.Description));

            }
            //Convierto la lista de los Products a ProductItemViewModel


        }
        #endregion

        #region Commands

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(LoadExercises);
            }
        }
        //carga los productos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }
        #endregion

    }
}
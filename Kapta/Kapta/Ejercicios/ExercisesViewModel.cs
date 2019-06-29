using GalaSoft.MvvmLight.Command;
using Kapta.Common.Models;
using Kapta.Herramientas.Helpers;
using Kapta.Herramientas.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kapta.Ejercicios
{
    public class ExercisesViewModel : BaseViewModel
    {
        #region Attributes
        private string filter;

        private APIService apiService;

        private DataService dataService;

        private bool isRefreshing;

        private ObservableCollection<ExerciseItemViewModel> exercises;
        #endregion

        #region Properties

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

        public ObservableCollection<ExerciseItemViewModel> Exercises
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
        public ExercisesViewModel()
        {
            instance = this;
            this.apiService = new APIService();
            this.dataService = new DataService();
            this.LoadExercises();
        }
        #endregion

        #region Singleton
        private static ExercisesViewModel instance;

        public static ExercisesViewModel GetInstance()
        {
            if (instance == null)
            {
                return new ExercisesViewModel();
            }
            return instance;
        }

        #endregion
        //Cargar ejercicios
        #region Methods
        private async void LoadExercises()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (connection.IsSuccess)
            {
                var answer = await this.LoadExercisesFromAPI();
                if (answer)
                {
                    this.SaveExercisesToDB();
                }
            }
            else
            {
                await this.LoadExercisesFromDB();
            }

            if(this.MyExercises ==null || this.MyExercises.Count == 0)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoProductsMessage, Languages.Accept);
                return;
            }
            /*
            this.IsRefreshing = false;
            await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
            return;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlExercisesController"].ToString();

            var response = await this.apiService.GetList<Exercise>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            this.MyExercises = (List<Exercise>)response.Result;
            */
            this.RefreshList();
            
            this.IsRefreshing = false;

        }

        private async Task LoadExercisesFromDB()
        {
            this.MyExercises = await this.dataService.GetAllExercises();
        }

        private async void SaveExercisesToDB()
        {
            await this.dataService.DeleteAllExercises();
            this.dataService.Insert(this.MyExercises);
        }

        private async Task<bool> LoadExercisesFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlExercisesController"].ToString();

            var response = await this.apiService.GetList<Exercise>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
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
                var myListExerciseItemViewModel = this.MyExercises.Select(p => new ExerciseItemViewModel
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
                    //CategoryId = p.CategoryId,
                    //UserId = p.UserId,

                });

                this.Exercises = new ObservableCollection<ExerciseItemViewModel>(
                    myListExerciseItemViewModel.OrderBy(p => p.Name));
            }
            else
            {

                var myListExerciseViewModel = MyExercises.Select(p => new ExerciseItemViewModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                    ExerciseId = p.ExerciseId,
                    PublishOn = p.PublishOn,

                }).Where(p => p.Name.ToLower().Contains(this.Filter.ToLower())).ToList(); ;

                this.Exercises = new ObservableCollection<ExerciseItemViewModel>(
                    myListExerciseViewModel.OrderBy(p => p.Name));
            }
            
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadExercises);
            }
        }
        #endregion

    }
}

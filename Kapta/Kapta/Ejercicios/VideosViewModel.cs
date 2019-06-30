namespace Kapta.Ejercicios
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using GalaSoft.MvvmLight.Command;
    using Kapta;
    using Kapta.Common.Models;
    using Kapta.Herramientas.Helpers;
    using Kapta.Herramientas.Services;
    using Xamarin.Forms;

    public class VideosViewModel : BaseViewModel
    {
        #region Attributes
        private string filter;

        private APIService apiService;

        private bool isRefreshing;

        private ObservableCollection<VideoItemViewModel> videos;
        #endregion

        #region Properties
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                this.RefreshList();
            }
        }

        public List<Video> MyVideos { get; set; }

        public ObservableCollection<VideoItemViewModel> Videos
        {
            get { return this.videos; }
            set { this.SetValue(ref this.videos, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public VideosViewModel()
        {
            this.apiService = new APIService();
            this.LoadCategories();
        }
        #endregion

        #region Methods
        private async void LoadCategories()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlVideosController"].ToString();
            var response = await this.apiService.GetList<Video>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            this.MyVideos = (List<Video>)response.Result;
            this.RefreshList();
            this.IsRefreshing = false;
        }

        private void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myListVideosItemViewModel = this.MyVideos.Select(c => new VideoItemViewModel
                {
                    VideoId = c.VideoId,
                    NombreVideo = c.NombreVideo,
                    LinkVideo = c.LinkVideo,
                    Description = c.Description,
                    ImagePath = c.ImagePath,
                });
                //Arma la observablecollection y comprueba si hay filtro
                this.Videos = new ObservableCollection<VideoItemViewModel>(
                    myListVideosItemViewModel.OrderBy(c => c.NombreVideo));
            }
            else
            {
                var myListVideosItemViewModel = this.MyVideos.Select(c => new VideoItemViewModel
                {
                    VideoId = c.VideoId,
                    NombreVideo = c.NombreVideo,
                    LinkVideo = c.LinkVideo,
                    Description = c.Description,
                    ImagePath = c.ImagePath,
                }).Where(c => c.NombreVideo.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Videos = new ObservableCollection<VideoItemViewModel>(
                    myListVideosItemViewModel.OrderBy(c => c.NombreVideo));
            }
        }
        #endregion

        #region Commands
        //refresca la lista
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }

        //carga la lista de categorias
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCategories);
            }
        }
        #endregion
    }

}

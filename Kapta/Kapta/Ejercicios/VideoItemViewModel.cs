using System;
using System.Collections.Generic;
using System.Text;

namespace Kapta.Ejercicios
{
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;


    //hereda de video para mantener el modelo video puro sin métodos
    public class VideoItemViewModel : Video
    {
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
           // MainViewModel.GetInstance().Products = new ProductsViewModel(this);// (this);
            await App.Navigator.PushAsync(new VerVideo(this));
        }
        #endregion
    }

}

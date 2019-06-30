namespace Kapta
{
    using GalaSoft.MvvmLight.Command;
    using Kapta.Categorias;
    using Kapta.Common.Models;
    using Kapta.Contacto;
    using Kapta.Ejercicios;
    using Kapta.Herramientas.Helpers;
    using Kapta.Usuarios;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }

        public CategoriesViewModel Categories { get; set; }


        public VideosViewModel Videos { get; set; }

        public CategoriesViewModelUser Categoriess { get; set; }

        public EditExerciseViewModel EditExercise { get; set; }

        public EditExerciseMessageViewModel EditProductt { get; set; }

        public ExercisesViewModel Exercises { get; set; }

        public ExercisesViewModelUser Exercisess { get; set; }

        public AddExerciseViewModel AddExercise { get; set; }

        public RegisterViewModel Register { get; set; }

        public ContactViewModel Contact { get; set; }

        public MyUserASP UserASP { get; set; }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public string UserFullName
        {
            get
            {
                //Si el usuario no es nulo y los datos claims son mayores que uno
                if (this.UserASP != null && this.UserASP.Claims != null && this.UserASP.Claims.Count > 1)
                {
                    //devolvemos claim 0 y claim 1 que es el nombre y apellidos
                    return $"{this.UserASP.Claims[0].ClaimValue} {this.UserASP.Claims[1].ClaimValue}";
                }

                return null;
            }
        }

        public string UserImageFullPath
        {
            get
            {
                foreach (var claim in this.UserASP.Claims)
                {
                    if (claim.ClaimType == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri")
                    {
                        if (claim.ClaimValue.StartsWith("~"))
                        {
                            return $"http://kaptaapi.azurewebsites.net{claim.ClaimValue.Substring(1)}";
                        }

                        return claim.ClaimValue;
                    }
                }

                return "nouser";
            }
        }

        #endregion


        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.LoadMenu();
            //this.Exercises = new ExercisesViewModel();
        }
        #endregion

        //opciones del menú
        #region Methods

        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            /*
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "message",
                PageName = "Presentation",
                Title = Languages.Presentation,
            
            */
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "user",
                PageName = "Paciente",
                Title = "Gestión de deportistas",
            });
            /*
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_info",
                PageName = "AboutPage",
                Title = Languages.About,
            });
            */

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "contacts",
                PageName = "Exercises",
                Title = "Ejercicios",
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "videos",
                PageName = "Videos",
                Title = "Videos",
            });


            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "information",
                PageName = "Contact",
                Title = "Contacto",
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "iconfinanciacion",
                PageName = "Financiacion",
                Title = "Financiación",
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "exit",
                PageName = "LoginPage",
                Title = Languages.Exit,
            });



        }


        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion
        #region Commands
        //cuando toque en el icono +, lanza el comando AddProductComand, que devuelve la pagina del metodo GoToAddProduct
        public ICommand AddExerciseCommand
        {
            get
            {
                return new RelayCommand(GoToAddExercise);
            }
        }
        
        private async void GoToAddExercise()
        {
            //Antes de lanzar la pagina instanciamos la viewmodel que gobierna la pagina
            this.AddExercise = new AddExerciseViewModel();
            await App.Navigator.PushAsync(new AddExercisePage());
            //await Application.Current.MainPage.Navigation.PushAsync(new AddExercisePage());
            //await ApnewExercise.Navigator.PushAsync(new AddExercisePage());
        }
        
        #endregion
    }
}

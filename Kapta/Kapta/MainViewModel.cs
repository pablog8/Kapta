namespace Kapta
{
    using Kapta.Ejercicios;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MainViewModel
    {
        public ExercisesViewModel Exercises { get; set; }

        public MainViewModel()
        {
            this.Exercises = new ExercisesViewModel();
        }
    }
}

namespace Kapta.Backend.Models
{
    using Kapta.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ExerciseView : Exercise
    {
        //heredamos del producto para agregar un nuevo atributo al modelo
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
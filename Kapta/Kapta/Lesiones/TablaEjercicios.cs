//using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kapta.Lesiones
{
    public class TablaEjercicios
    {

        //[PrimaryKey, AutoIncrement]
        public int IDEjercicio { get; set; }
        public int clavedeportista { get; set; }
        public string Nombreejercicio { get; set; }
        public string ComentarioEjercicio { get; set; }
        public string Descripcion { get; set; }
        public int Series { get; set; }
    }
}
//using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kapta.Lesiones
{
    public class Lesion
    {
        //[PrimaryKey, AutoIncrement]
        public int IDLesion { get; set; }
        public int clavedeportista { get; set; }
        public string Miembro { get; set; }
        public string Lugar { get; set; }
        public string Tipo { get; set; }
        public int NumLesiones { get; set; }
    }
}
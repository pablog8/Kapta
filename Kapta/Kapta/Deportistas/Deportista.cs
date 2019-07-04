using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kapta.Deportistas
{
    public class Deportista
    {
        [PrimaryKey, AutoIncrement]
        public int IDDeportista { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string IdUser { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        // public decimal Salario { get; set; }
        public bool Activo { get; set; }

        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1}", this.Nombres, this.Apellidos);
            }
        }
        public string EmailEditado
        {
            get
            {
                return string.Format("{0}", this.Email);
            }
        }

        public string FechaNacimientoEditada
        {
            get
            {
                return string.Format("{0:dd-MM-yyyy}", FechaNacimiento);
            }
        }
        /*
        public string SalarioEditado
        {
            get
            {
                return string.Format("{0:C2}", Salario);
            }
        }
        */
        public override string ToString()
        {

            return string.Format("{0} {1} {2}", IDDeportista, NombreCompleto, FechaNacimientoEditada);

        }
    }
}

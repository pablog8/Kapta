using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Kapta.Common.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using System.Threading.Tasks;

    public class Prueba
    {
        [Key]
        public int PruebaId { get; set; }


        [Required]
        public string Name { get; set; }

       
        public string Description { get; set; }

        // public Decimal Price { get; set; }
        // public bool IsAvailable { get; set; }
        //para tener atributos que no esten en la base de datos pero si en el modelo
        
    }
}
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

    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [Display(Name = "Imagen")]
        public string ImagePath { get; set; }

        //display para cómo quieres que lo vea el usuario en el backend
        [Display(Name = "Publicación")]
        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }
        
        [JsonIgnore]
        public virtual Category Category { get; set; }
        
        // public Decimal Price { get; set; }
        // public bool IsAvailable { get; set; }
        //para tener atributos que no esten en la base de datos pero si en el modelo
        [NotMapped]
        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noexercise.png";
                }
                //devuelve la imagen (pagina de backend)
                //return $"http://kaptabackend.azurewebsites.net/{this.ImagePath.Substring(1)}";
                return $"http://kaptaapi.azurewebsites.net/{this.ImagePath.Substring(1)}";
            }
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}

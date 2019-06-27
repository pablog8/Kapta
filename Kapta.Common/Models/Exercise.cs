using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Kapta.Common.Models
{
    using System.Text;
    using System.Threading.Tasks;

    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

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

        // public Decimal Price { get; set; }
        // public bool IsAvailable { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}

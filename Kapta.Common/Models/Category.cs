using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapta.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        [Display(Name = "Id Categoría")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Imagen")]
        public string ImagePath { get; set; }

        [JsonIgnore]
        public virtual ICollection<Exercise> Exercises { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noproduct";
                }

                return $"http://kaptabackend.azurewebsites.net{this.ImagePath.Substring(1)}";
            }
        }
    }

}

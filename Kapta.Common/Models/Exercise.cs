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
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime PublishOn { get; set; }

        // public Decimal Price { get; set; }
        // public bool IsAvailable { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}

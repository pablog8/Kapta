namespace Kapta.Domain.Models

{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<Common.Models.Category> Categories { get; set; }
        public DbSet<Common.Models.Exercise> Exercises { get; set; }
    }
}

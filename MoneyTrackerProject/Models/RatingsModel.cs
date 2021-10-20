using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MoneyTrackerProject.Models
{
    public partial class RatingsModel : DbContext
    {
        public RatingsModel()
            : base("name=RatingsModel")
        {
        }

        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

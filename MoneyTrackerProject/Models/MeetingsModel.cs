using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MoneyTrackerProject.Models
{
    public partial class MeetingsModel : DbContext
    {
        public MeetingsModel()
            : base("name=MeetingsModel")
        {
        }

        public virtual DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

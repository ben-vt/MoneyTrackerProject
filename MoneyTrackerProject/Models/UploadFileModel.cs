using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MoneyTrackerProject.Models
{
    public partial class UploadFileModel : DbContext
    {
        public UploadFileModel()
            : base("name=UploadFileModel")
        {
        }

        public virtual DbSet<Upload_Files> Upload_Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Upload_Files>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Upload_Files>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}

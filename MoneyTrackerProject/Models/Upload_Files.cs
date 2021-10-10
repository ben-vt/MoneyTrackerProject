namespace MoneyTrackerProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Upload_Files
    {
        public int Id { get; set; }

        [Required]
        [StringLength(259)]
        public string Path { get; set; }

        [Required(ErrorMessage = "Please provide a name for the file.")]
        [StringLength(200)]
        public string Name { get; set; }
    }
}

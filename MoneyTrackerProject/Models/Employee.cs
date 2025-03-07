//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MoneyTrackerProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int EmployeeId { get; set; }
        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "Please provide the employee's name.")]
        public string EmployeeName { get; set; }

        [DisplayName("Position")]
        [Required(ErrorMessage = "Please provide the employee's role.")]
        public string EmployeeRole { get; set; }
        public int DeptId { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please provide the employee's email address.")]
        public string EmployeeEmail { get; set; }

        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

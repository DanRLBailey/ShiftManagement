using System.ComponentModel.DataAnnotations;

namespace Shift_Management.Models
{
    public class EmployeeModel
    {
        [Display(Name = "Employee ID")]
        public int Employee_ID { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
    }
}

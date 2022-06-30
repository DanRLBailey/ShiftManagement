using System.ComponentModel.DataAnnotations;

namespace Shift_Management.Models
{
    public class ShiftManagementModel
    {
        public EmployeeModel? Employee { get; set; }
        public List<EmployeeWorksShiftModel>? EmployeeShifts { get; set; }
        public List<ShiftModel>? Shifts { get; set; }
        public List<int>? EmployeeShiftotalHours { get; set; }
    }
}

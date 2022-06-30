using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shift_Management.Models;

namespace Shift_Management.Controllers
{
    public class ShiftManagementController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var shiftManagement = new List<ShiftManagementModel>();
            var employees = await GetEmployees();

            for (int i = 0; i < employees.Count; i++)
            {
                EmployeeModel? e = employees[i];
                var employeeShifts = await GetWorkedShifts(e.Employee_ID);
                var shifts = await GetShiftDetails(employeeShifts);

                shiftManagement.Add(new ShiftManagementModel
                {
                    Employee = e,
                    EmployeeShifts = employeeShifts,
                    Shifts = shifts,
                    EmployeeShiftotalHours = GetTotalHoursByMonth(shifts)
                });
            }

            return View(shiftManagement);
        }

        public async Task<List<EmployeeModel>?> GetEmployees()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var rsp = await client.GetAsync("https://localhost:7156/api/Employee");
                    var rspContent = await rsp.Content.ReadAsStringAsync();
                    var employeeList = JsonConvert.DeserializeObject<List<EmployeeModel>>(rspContent);

                    return employeeList;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        public async Task<List<EmployeeWorksShiftModel>?> GetWorkedShifts(int employeeId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var rsp = await client.GetAsync("https://localhost:7156/api/EmployeeShift");
                    var rspContent = await rsp.Content.ReadAsStringAsync();
                    var employeeShiftList = JsonConvert.DeserializeObject<List<EmployeeWorksShiftModel>>(rspContent);

                    return employeeShiftList.Where(s => s.Employee_ID == employeeId).ToList();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public async Task<List<ShiftModel>?> GetShiftDetails(List<EmployeeWorksShiftModel> shifts)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var rsp = await client.GetAsync("https://localhost:7156/api/Shift");
                    var rspContent = await rsp.Content.ReadAsStringAsync();
                    var shiftList = JsonConvert.DeserializeObject<List<ShiftModel>>(rspContent);

                    return shiftList.Where(s => shifts.Any(x => x.Shift_ID == s.Shift_ID)).ToList();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        static public List<int> GetTotalHoursByMonth(List<ShiftModel> shifts)
        {
            if (shifts.Count == 0) 
                return new List<int>();

            var hours = new List<int>();
            var hourList = new List<int>();

            for (int i = 0; i < shifts.Count; i++)
            {
                ShiftModel shift = shifts[i];

                if ((i > 0 && shift.Shift_Start.Month != shifts[i - 1].Shift_Start.Month))
                {
                    hours.Add(hourList.Sum());
                    hourList = new List<int>();
                    hourList.Add(GetTimeBetweenDates(shift.Shift_Start, shift.Shift_End));
                }
                else
                {
                    hourList.Add((int)(shift.Shift_End - shift.Shift_Start).TotalHours);
                }

                //hours.Add((int)(shift.Shift_End - shift.Shift_Start).TotalHours);
            }
            hours.Add(hourList.Sum());
            return hours;
        }

        static public int GetTimeBetweenDates(DateTime shiftStart, DateTime shiftEnd)
        {
            return (shiftStart != DateTime.MinValue & shiftEnd != DateTime.MinValue)
                ? (int)(shiftEnd - shiftStart).TotalHours :
                0 ;
        }
    }
}

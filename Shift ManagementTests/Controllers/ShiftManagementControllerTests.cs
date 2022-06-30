using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shift_Management.Controllers;
using Shift_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shift_Management.Controllers.Tests
{
    [TestClass()]
    public class ShiftManagementControllerTests
    {
        [TestMethod()]
        public void GetEmployeeHoursTest()
        {
            var startDate = DateTime.Parse("2016-11-11 09:00:00.000");
            var endDate = DateTime.Parse("2016-11-11 17:00:00.000");
            var expected = 8;

            var actual = ShiftManagementController.GetTimeBetweenDates(startDate, endDate);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetEmployeeHoursEmptyTest()
        {
            var startDate = new DateTime();
            var endDate = new DateTime();
            var expected = 0;

            var actual = ShiftManagementController.GetTimeBetweenDates(startDate, endDate);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalHoursByMonthTest()
        {
            var expected = new List<int> { 8 };
            var shiftList = new List<ShiftModel>
            {
                new ShiftModel
                {
                    Shift_ID = 1,
                    Shift_Start = DateTime.Parse("2016-11-11 09:00:00.000"),
                    Shift_End = DateTime.Parse("2016-11-11 17:00:00.000"),
                    Shift_Name = "Morning 9-17"
                }
            };

            var actual = ShiftManagementController.GetTotalHoursByMonth(shiftList);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalHoursByMonthTest2()
        {
            var expected = new List<int> { 12, 8 };
            var shiftList = new List<ShiftModel>
            {
                new ShiftModel
                {
                    Shift_ID = 1,
                    Shift_Start = DateTime.Parse("2016-11-11 09:00:00.000"),
                    Shift_End = DateTime.Parse("2016-11-11 17:00:00.000"),
                    Shift_Name = "Morning 9-17"
                },
                new ShiftModel
                {
                    Shift_ID = 2,
                    Shift_Start = DateTime.Parse("2016-11-12 10:00:00.000"),
                    Shift_End = DateTime.Parse("2016-11-12 14:00:00.000"),
                    Shift_Name = "Morning 10-14"
                },
                new ShiftModel
                {
                    Shift_ID = 7,
                    Shift_Start = DateTime.Parse("2016-12-13 09:00:00.000"),
                    Shift_End = DateTime.Parse("2016-12-13 17:00:00.000"),
                    Shift_Name = "Morning 9-17"
                }
            };
            
            var actual = ShiftManagementController.GetTotalHoursByMonth(shiftList);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTotalHoursByMonthEmptyTest()
        {
            var expected = new List<int>();
            var shiftList = new List<ShiftModel>();

            var actual = ShiftManagementController.GetTotalHoursByMonth(shiftList);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
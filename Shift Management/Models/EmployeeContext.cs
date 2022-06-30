using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Shift_Management.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<EmployeeModel> Employee { get; set; } = null!;
        public DbSet<EmployeeWorksShiftModel> Employee_Works_Shift { get; set; } = null!;
        public DbSet<ShiftModel> Shifts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>()
                .HasKey(e => e.Employee_ID);
            modelBuilder.Entity<EmployeeWorksShiftModel>()
                .HasNoKey();
            modelBuilder.Entity<ShiftModel>()
                .HasKey(s => s.Shift_ID);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Shift_Management.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddControllersAsServices();
builder.Services.AddDbContext<EmployeeContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeManagementContext") ?? throw new InvalidOperationException("Connection string 'EmployeeManagementContext' not found.")));

var app = builder.Build();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ShiftManagement}/{action=Index}/{id?}");

app.Run();

using DotNetProject.Data_Access.Implementation;
using DotNetProject.Data_Access.Interfaces;
using DotNetProject.DataFolder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddHsts(hsts =>
//{
//    hsts.MaxAge = TimeSpan.FromDays(2);

//});

builder.Services.AddDbContext<AppDbContext>(option => option.UseNpgsql(connectionString));


//todo
//builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<DotNetProject.Data_Access.Interfaces.ICourseRepository, DotNetProject.Data_Access.Implementation.CourseRepository>();
builder.Services.AddScoped<DotNetProject.Data_Access.Interfaces.IStudentRepository, DotNetProject.Data_Access.Implementation.StudentRepository>();
builder.Services.AddScoped<DotNetProject.Data_Access.Interfaces.IEnrollmentRepository, DotNetProject.Data_Access.Implementation.EnrollmentRepository>();


//builder.Services.AddScoped(typeof(IRepository<TEntity, TPrimaryKey>), typeof(Repository<TEntity, TPrimaryKey>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
  pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();

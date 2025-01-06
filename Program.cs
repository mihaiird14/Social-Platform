using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Social_Life.Data;
using Social_Life.Models;

<<<<<<< HEAD
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
=======

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

<<<<<<< HEAD
// Configure Identity with roles
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

=======
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

<<<<<<< HEAD
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

=======
>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
<<<<<<< HEAD
    app.UseHsts();
}

=======
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


>>>>>>> a2513c7058a54005c60095c78ee76cac5eedb1fc
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Profile}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

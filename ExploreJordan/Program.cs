using ExploreJordan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ExploreJordan.ExploreJordan.utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using ExploreJordan.Services;
using ExploreJordan.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IEmailSender,EmailSender>();
builder.Services.AddScoped<ICarsServices, CarsServices>();
builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<ISouvenirServices, SouvenirServices>();
builder.Services.AddScoped<IAccommodationServices, AccommodationServices>();
builder.Services.AddScoped<IActivitiesServices, ActivitiesServices>();
builder.Services.AddScoped<IToursServices, ToursServices>();



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddRazorPages();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();




app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Homepage}/{id?}");

app.MapControllerRoute(
    name: "carDetails",
    pattern: "{controller=Cars}/{action=Details}/{id?}");

app.MapControllerRoute(
	name: "Aboutus",
	pattern: "{controller=Home}/{action=aboutus}/{id?}");

app.MapControllerRoute(
    name: "PopularDestinations",
    pattern: "{controller=Home}/{action=aboutus}/{id?}");

app.MapControllerRoute(
	name: "Contactus",
	pattern: "{controller=PopularDestinations}/{action=Amman}/{id?}");

app.MapControllerRoute(
	name: "Blog",
	pattern: "{controller=Blog}/{action=Blog}/{id?}");

app.MapControllerRoute(
	name: "Blog1",
	pattern: "{controller=Blog}/{action=Blog1}/{id?}");

app.MapControllerRoute(
	name: "Blog2",
	pattern: "{controller=Blog}/{action=Blog2}/{id?}");

app.MapControllerRoute(
	name: "Blog3",
	pattern: "{controller=Blog}/{action=Blog3}/{id?}");

app.MapControllerRoute(
	name: "Blog4",
	pattern: "{controller=Blog}/{action=Blog4}/{id?}");

app.MapControllerRoute(
	name: "Blog5",
	pattern: "{controller=Blog}/{action=Blog5}/{id?}");

app.MapControllerRoute(
	name: "Blog6",
	pattern: "{controller=Blog}/{action=Blog6}/{id?}");

app.MapControllerRoute(
    name: "cars",
    pattern: "{controller=Cars}/{action=cars}/{id?}");

app.MapControllerRoute(
    name: "souvenirs",
    pattern: "{controller=Souvenirs}/{action=souvenirs}/{id?}");

app.MapControllerRoute(
    name: "accommodation",
    pattern: "{controller=Accommodation}/{action=accommodation}/{id?}");

app.MapControllerRoute(
    name: "tours",
    pattern: "{controller=Tours}/{action=tours}/{id?}");

app.MapControllerRoute(
    name: "activities",
    pattern: "{controller=Activities}/{action=activities}/{id?}");

app.MapControllerRoute(
    name: "edit",
    pattern: "{controller=Cars}/{action=Edit}/{id?}");

app.MapControllerRoute(
    name: "edits",
    pattern: "{controller=Souvenirs}/{action=Edit}/{id?}");

app.MapControllerRoute(
    name: "edita",
    pattern: "{controller=Accommodation}/{action=Edit}/{id?}");

app.MapControllerRoute(
    name: "editt",
    pattern: "{controller=Tours}/{action=Edit}/{id?}");

app.MapControllerRoute(
    name: "editac",
    pattern: "{controller=Activities}/{action=Edit}/{id?}");

app.MapControllerRoute(
    name: "combined",
    pattern: "{controller=Combined}/{action=combined}/{id?}");

app.MapControllerRoute(
    name: "search",
    pattern: "{controller=Combined}/{action=Search}/{id?}");

app.Run();

using Microsoft.EntityFrameworkCore;
using PieShop.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository,PieRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


//Add dbcontext to the program
builder.Services.AddDbContext<PieShopDbContext>(options => {
	options.UseSqlServer(
		builder.Configuration["ConnectionStrings:PieShopDbContextConnection"]);
});

//This indicates that we are going to work with MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

//app. is to add Middleware

//To access static files from www folder
app.UseStaticFiles();

app.UseSession();

if (app.Environment.IsDevelopment())
{
	//To be able to log errors and exceptions in the browser
	app.UseDeveloperExceptionPage();
}

//set the app to work matching paths with the format "<domain>/{Controller=Home}/{View=Index}/{Id?}"
app.MapDefaultControllerRoute();

//This is exactly the same as app.MapDefaultControllerRoute(); It also defines the default path if it is not given in the route (Home/Index)
/*
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}" // "{controller=Home}/{action=Index}/{id:int?}"
	);
*/
//Seed the DB if it is empty
DbInitializer.Seed(app);

app.Run();

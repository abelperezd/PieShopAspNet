using Microsoft.EntityFrameworkCore;
using PieShop.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


//Add dbcontext to the program
builder.Services.AddDbContext<PieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:PieShopDbContextConnection"]);
});

//This would be only needed if we built an ASP.NET Core Api. MVC already includes this
//builder.Services.AddControllers();

//This indicates that we are going to work with Razor PAges
builder.Services.AddRazorPages();
//This indicates that we are going to work with MVC
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
        {
            //when serializing to send the data as JSON (from the Api), avoid infinite loops of references
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });

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

//This would be only needed if we built an ASP.NET Core Api. MVC already includes this
//app.MapControllers();

//enables razor pages model
app.MapRazorPages();
//Seed the DB if it is empty
DbInitializer.Seed(app);

app.Run();

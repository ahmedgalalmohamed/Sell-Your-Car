using projectcar.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CarContext>();
builder.Services.AddSession();
builder.Services.AddMvc(ops => ops.EnableEndpointRouting = false);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    string path = context.Request.Path;
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/Index";
        context.Response.Redirect(context.Request.Path);
        await next();
    }
});


app.UseAuthorization();

app.UseSession();


app.UseMvc(routes =>
{
    routes.MapRoute("default", "{controller=Home}/{action=Index}");
});

app.Run();

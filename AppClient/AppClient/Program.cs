var builder = WebApplication.CreateBuilder(args);

// Configure HttpClient with a named client "ApiClient"
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7046/api/"); // Replace with your API base URL
});

// Add MVC services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Use routing, static files, etc.
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

// Configure the default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();

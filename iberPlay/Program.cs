var builder = WebApplication.CreateBuilder(args);

// Seleccionar un puerto aleatorio
var randomPort = new Random().Next(5000, 9000);
builder.WebHost.UseUrls($"http://localhost:{randomPort}");

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

Console.WriteLine($"Servidor iniciado en http://localhost:{randomPort}");

app.Run();
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using backend.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<backendContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("backendContext") ?? throw new InvalidOperationException("Connection string 'backendContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Asegúrate de especificar el origen correcto
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

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

// Use CORS policy
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

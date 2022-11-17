using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDB>(options =>
    {
        options.UseSqlServer(
           @"Server=127.0.0.1;Database=LibraryDb;MultipleActiveResultSets=true;User=sa;Password=P@ssword1;Encrypt=False",
            b => b.MigrationsAssembly(typeof(Program).Assembly.FullName));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
/* app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"); */

app.Run();

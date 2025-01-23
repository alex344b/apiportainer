using apiapp.Data;
using apiapp.Controllers;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy => { policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }
    );
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration Configuration = builder.Configuration;

string connectionString = Configuration.GetConnectionString("DefaultConnection")
                          ?? Environment.GetEnvironmentVariable("DefaultConnection");



builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/", async context => 
{
    context.Response.Redirect("/swagger");
    await Task.CompletedTask;
});

app.Run();
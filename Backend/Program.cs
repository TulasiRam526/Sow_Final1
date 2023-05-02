using CandidateSoW.Models;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables().Build();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllCors",builder =>
                      {
                          builder.AllowAnyOrigin();
                          builder.AllowAnyMethod();
                          builder.AllowAnyHeader();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

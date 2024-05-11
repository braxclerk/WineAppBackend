using System;
using APApiDbS2024InClass.DataRepository;
//using APApiDbS2024InClass.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddScoped<UserRepository>();



// Add services to the container.
builder.Services.AddScoped(provider => {
  var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
  return new WineRepository(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});




var app = builder.Build();

app.UseCors(builder => {
  builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


//app.UseHeaderAuthenticationMiddleware();
//app.UseBasicAuthenticationMiddleware();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRouting();
app.MapControllers();
app.UseStaticFiles();
app.Run();
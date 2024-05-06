using System;
using APApiDbS2024InClass.DataRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>(provider => {
  // Retrieve the connection string from configuration
  var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
  return new ReviewRepository(connectionString);
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



//app.UseHttpsRedirection();

//app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.UseStaticFiles();
app.Run();
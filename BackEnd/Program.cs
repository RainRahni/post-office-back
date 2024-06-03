using Microsoft.EntityFrameworkCore;
using post_office_back.Data;
using post_office_back.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ShipmentService>();
builder.Services.AddScoped<ValidationService>();
builder.Services.AddScoped<BagService>();
builder.Services.AddScoped<IDataContext, DataContext>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<ParcelService>();
builder.Services.AddTransient<GlobalExceptionHandler>();    
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors(AllowSpecificOrigins);

app.UseMiddleware<GlobalExceptionHandler>();   

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

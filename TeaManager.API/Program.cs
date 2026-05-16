using Microsoft.EntityFrameworkCore;
using TeaManager.API.Data;
using TeaManager.API.Middleware;
using TeaManager.API.Repositories;
using TeaManager.API.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:5173")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<TeaManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TeaManagerDbConnectionString")));
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IBrandRepository, BrandRepository>(); //generate new instance when requesting (HTTP request scope)
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);//apply MyAllowSpecificOrigins policy
app.UseAuthorization();

app.MapControllers();

app.Run();

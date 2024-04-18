using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Service;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// .
// Add DbContext to the service container with the specified connection string
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Configure the DbContext to use SQL Server with the connection string retrieved from configuration
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure JwtOptions settings using configuration data
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();

// Register the JwtTokenGenerator service to generate JWT tokens for authentication.
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Registering AuthService with the dependency injection container
builder.Services.AddScoped<IAuthService, AuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Method to apply pending migrations to the database
ApplyMigration();

app.Run();

void ApplyMigration()
{
    // Create a scope to resolve services from the application's service provider
    using (var scope = app.Services.CreateScope())
    {
        // Get the instance of the application's database context
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Check if there are pending migrations
        if (_db.Database.GetPendingMigrations().Any())
        {
            // Apply pending migrations
            _db.Database.Migrate();
        }
    }
}
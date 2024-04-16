using Mango.Services.AuthAPI.Data;

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

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();
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
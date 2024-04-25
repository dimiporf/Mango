using AutoMapper;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Add DbContext to the service container with the specified connection string
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Configure the DbContext to use SQL Server with the connection string retrieved from configuration
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register AutoMapper with mapping configurations
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

// Add IMapper as a singleton service to the dependency injection container
builder.Services.AddSingleton(mapper);

// Add AutoMapper with assemblies to the dependency injection container
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(option =>
{
    // Add a security definition for Bearer token authentication
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization", // Name of the header to be used
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`", // Description displayed in Swagger UI
        In = ParameterLocation.Header, // Location of the API key (in the header)
        Type = SecuritySchemeType.ApiKey, // Type of security scheme (ApiKey)
        Scheme = "Bearer" // Authentication scheme (Bearer)
    });

    // Add a security requirement for Bearer token authentication
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme // Reference to the defined security scheme
                }
            },
            new string[] {} // Empty array indicates no specific scopes are required
        }
    });
});

// Parse the ApiSettings Section into a variable to demonstrate 'GetSection' method
var settingsSection = builder.Configuration.GetSection("ApiSettings");


// Retrieve JWT secret, issuer, and audience from configuration
var secret = settingsSection.GetValue<string>("Secret");
var issuer = settingsSection.GetValue<string>("Issuer");
var audience = settingsSection.GetValue<string>("Audience");

// Convert the secret into a byte array
var key = Encoding.ASCII.GetBytes(secret);

// Add JWT authentication services to the DI container
builder.Services.AddAuthentication(x =>
{
    // Specify the default authentication scheme for both authentication and challenge responses
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    // Configure the JWT bearer options
    x.TokenValidationParameters = new TokenValidationParameters
    {
        // Enable validation of the JWT signature using the specified key
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        // Validate the JWT issuer (if specified) against the expected issuer
        ValidateIssuer = true,
        ValidIssuer = issuer,

        // Validate the JWT audience (if specified) against the expected audience
        ValidAudience = audience,
        ValidateAudience = true
    };
});

// Add authorization services to the DI container
builder.Services.AddAuthorization();


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

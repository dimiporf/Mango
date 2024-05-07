using Mango.Web.Service;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the HttpContextAccessor to the service collection to access HttpContext in services.
builder.Services.AddHttpContextAccessor();

// Add HttpClient service to the service collection for making HTTP requests.
builder.Services.AddHttpClient();

// Register HttpClient for ICouponService with CouponService implementation
builder.Services.AddHttpClient<ICouponService, CouponService>();

// Register HttpClient for IProductService with ProductService implementation
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<ICartService, CartService>();

// Register HttpClient for IAuthService with AuthService implementation
builder.Services.AddHttpClient<IAuthService, AuthService>();

// Set the CouponAPIBase URL from configuration to the SD utility class.
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];

// Set the AuthAPIBase URL from configuration to the SD utility class.
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];

// Set the Product and Cart APIBase URL from configuration to the SD utility class.
SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];

// Register IBaseService and ICouponService with their respective implementations
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();

// Register TokenProvider as a scoped service to manage token storage.
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

// Authentication registration to services, cookies options added
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// This is important to be implemented BEFORE authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

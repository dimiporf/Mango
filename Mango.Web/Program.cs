using Mango.Web.Service;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the HttpContextAccessor to the service collection to access HttpContext in services.
builder.Services.AddHttpContextAccessor();

// Add HttpClient service to the service collection for making HTTP requests.
builder.Services.AddHttpClient();

// Register HttpClient for ICouponService with CouponService implementation
builder.Services.AddHttpClient<ICouponService, CouponService>();


// Register HttpClient for IAuthService with AuthService implementation
builder.Services.AddHttpClient<IAuthService, AuthService>();

// Set the CouponAPIBase URL from configuration to the SD utility class.
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];

// Set the AuthAPIBase URL from configuration to the SD utility class.
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];

// Register IBaseService and ICouponService with their respective implementations
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Register TokenProvider as a scoped service to manage token storage.
builder.Services.AddScoped<ITokenProvider, TokenProvider>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

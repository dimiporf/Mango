# Mango.Web Microservices Project

This project is a microservices-based web application developed using ASP.NET Core. It integrates with various backend services using HttpClient to interact with different microservices. The application includes features for authentication, authorization, and communication with multiple APIs.

## Features

- **Microservices Integration**: Utilizes HttpClient to communicate with backend microservices, including services for coupons, authentication, products, and shopping carts.
  
- **Dependency Injection**: Registers HTTP clients for different services (`ICouponService`, `IProductService`, `ICartService`, `IAuthService`) and injects them into controllers and services using ASP.NET Core's dependency injection system.

- **Token-based Authentication**: Implements cookie-based authentication with `CookieAuthenticationDefaults.AuthenticationScheme`. Manages token storage and retrieval using `ITokenProvider`.

- **Configuration Management**: Retrieves service URLs from configuration settings (`appsettings.json`) and stores them in the `SD` utility class for easy access across the application.

- **Middleware Configuration**:
  - Enables HTTPS redirection (`UseHttpsRedirection`).
  - Serves static files (`UseStaticFiles`).
  - Configures routing for MVC controllers (`MapControllerRoute`).
  - Sets up exception handling and HSTS for production environments.

## Usage

To run the microservices project:

1. Clone this repository to your local machine.
2. Update the configuration settings in `appsettings.json` with appropriate URLs for your backend services (`CouponAPI`, `AuthAPI`, `ProductAPI`, `ShoppingCartAPI`).
3. Build and run the application using the following commands:
dotnet build
dotnet run
4. Access the web application in your browser at the specified URL.

### Important Notes

- Ensure that all backend microservices (`CouponAPI`, `AuthAPI`, `ProductAPI`, `ShoppingCartAPI`) are running and accessible from the web application.
- Customize the authentication settings (`ExpireTimeSpan`, `LoginPath`, `AccessDeniedPath`) in `AddCookie` options according to your application's requirements.

## Contributors

- Dimitrios Porfyropoulos - Backend Developer

![github2](https://github.com/dimiporf/Mango/assets/74142959/81d3b778-910c-4934-a88e-d0b7e828fd06)
![github1](https://github.com/dimiporf/Mango/assets/74142959/b5fd427d-6f02-41e2-b44d-3ef2d1767b86)

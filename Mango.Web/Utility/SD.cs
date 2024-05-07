namespace Mango.Web.Utility
{
    // Defines a utility class containing constants and enums used across the application.
    public class SD
    {
        // Base URL for the Coupon and Product APIs.
        public static string CouponAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }

        // Base URL for the Auth and Cart API.
        public static string AuthAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }

        // Constants for role configuration
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";

        // Constant for Token cookies
        public const string TokenCookie = "JWTToken";

        // Enumeration representing HTTP request types.
        public enum ApiType
        {
            GET,    // HTTP GET request
            POST,   // HTTP POST request
            PUT,    // HTTP PUT request
            DELETE  // HTTP DELETE request
        }
    }
}


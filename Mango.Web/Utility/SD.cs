namespace Mango.Web.Utility
{
    // Defines a utility class containing constants and enums used across the application.
    public class SD
    {
        // Base URL for the Coupon API.
        public static string CouponAPIBase { get; set; }

        // Base URL for the Auth API.
        public static string AuthAPIBase { get; set; }

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


namespace Mango.Services.AuthAPI.Models
{
    // Represents options for configuring JWT (JSON Web Token) settings
    public class JwtOptions
    {
        // The issuer of the JWT (e.g., the authorization server)
        public string Issuer { get; set; } = string.Empty;

        // The audience for the JWT (e.g., the intended recipients of the token)
        public string Audience { get; set; } = string.Empty;

        // The secret key used to sign the JWT
        public string Secret { get; set; } = string.Empty;
    }
}


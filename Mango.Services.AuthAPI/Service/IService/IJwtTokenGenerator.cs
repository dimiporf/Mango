using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Service.IService
{
    // Interface defining the contract for a JWT token generator service.
    public interface IJwtTokenGenerator
    {
        // Generates a JWT token for the specified ApplicationUser.
        string GenerateToken(ApplicationUser applicationUser);
    }
}


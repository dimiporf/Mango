namespace Mango.Web.Service.IService
{
    public interface ITokenProvider
    {
        // Sets the token in the provider.
        void SetToken(string token);

        // Retrieves the stored token from the provider.
        string? GetToken();

        // Clears the stored token from the provider.
        void ClearToken();
    }
}


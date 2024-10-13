using EclipseCapital.API.Models;


namespace EclipseCapital.API.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<(bool Success, string Message)> RegisterAsync(string username, string password);
    }
}

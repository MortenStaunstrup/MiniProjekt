// MiniProjektGenbrug/Services/Interfaces/IUserService.cs
using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniProjektGenbrug.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserLoggedIn();
        Task<User?> GetUserById(int? id);
        Task<User?> Login(string username, string password);
        Task<User> CreateUserAsync(string username, string email, string password);
        Task Logout();
    }
}
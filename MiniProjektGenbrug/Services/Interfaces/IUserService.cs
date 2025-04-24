using Core;

namespace MiniProjektGenbrug.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserLoggedIn();
    Task<User?> GetUserById(int id);
    Task<bool> Login(string username, string password);
    List<Product?> GetUserProducts(int userId);
    List<Product?> GetUserBuyHistory(int userId);
    
}
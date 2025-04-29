using Core;

namespace API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    
    Task<User?> GetUserById(int userId);

    Task AddUser(User user);

    Task<User?> GetUserByUsernameAndPassword(string username, string password);

    Task<List<Product>> GetProductsByUserId(int userId);

    Task<List<Product>> GetBuyHistoryByUserId(int userId);

    Task<int> GetMaxUserId();
    Task UpdateUser(User user);
}
using Core;

namespace MiniProjektGenbrug.Services.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(int userId, Product product);

        Task AddProductToCartAsync(int userId, Product product);
        Task<List<CartItem>> GetCartItemsAsync(int userId);
        Task RemoveFromCartAsync(int userId, int productId);
        Task ClearCartAsync(int userId);
    }
}
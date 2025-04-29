using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICartRepository
    {
        Task AddToCartAsync(CartItem item);
        
        Task AddToCart(CartItem item);
        Task<List<CartItem>> GetCartItemsByUserIdAsync(int userId);
        Task RemoveFromCartAsync(int cartItemId);
    }
}
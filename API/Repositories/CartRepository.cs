using Core;
using MongoDB.Driver;
using Services.Interfaces;

namespace Services
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<CartItem> _cartCollection;

        public CartRepository(IMongoDatabase database)
        {
            _cartCollection = database.GetCollection<CartItem>("Cart");
        }

        public async Task AddToCartAsync(CartItem item)
        {
            await _cartCollection.InsertOneAsync(item);
        }

        public Task AddToCart(CartItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CartItem>> GetCartItemsByUserIdAsync(int userId)
        {
            return await _cartCollection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task RemoveFromCartAsync(int cartId)
        {
            await _cartCollection.DeleteOneAsync(x => x.Id == cartId);
        }
    }
}
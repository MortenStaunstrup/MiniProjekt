using Core;

namespace MiniProjektGenbrug.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProducts();
    Task<Product> GetProductById(int id);
    Task<List<Product>?> GetBuyHistoryByUserId(int userId);
    Task<List<Product>?> GetProductsByUserId(int userId);
    void DeleteProductById(int id, int userId);
    void AddProduct(Product product, int userId);
    void UpdateProductById(int id, int userId, Product product);
    Task<bool> ExistsInOwnProducts(int productId, int buyerId);
    void AcceptBid(int productId, int sellerId);
    void DeclineBid(int productId, int sellerId);
    void BidOnProduct(int productId, int buyerId);
}
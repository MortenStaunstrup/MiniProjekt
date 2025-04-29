using Core;

namespace API.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProducts();
    Task<List<Product>?> GetProductsByUserId(int userId);
    Task<List<Product>?> GetBuyHistoryByUserId(int userId);
    Task<Product> GetProductById(int id);
    void BidOnProduct(int productId, int buyerId);
    void AcceptBid(int productId, int sellerId);
    void DeclineBid(int productId, int sellerId);
    void AddProduct(Product product, int userId);
    Task<int> GetMaxProductId();
    void UpdateProductById(int id, Product product, int userId);
    void DeleteProductById(int id, int userId);
}
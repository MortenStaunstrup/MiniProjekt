using Core;

namespace API.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProducts();
    Task<List<Product>?> GetProductsByUserId(int userId);
    Task<List<Product>?> GetBuyHistoryByUserId(int userId);
    Task<Product> GetProductById(int id);
    void AddProduct(Product product, int userId);
    Task<int> GetMaxProductId();
    void UpdateProductById(int id, Product product, int userId);
    void DeleteProductById(int id, int userId);
}
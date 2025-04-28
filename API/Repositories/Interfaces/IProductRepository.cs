using Core;

namespace API.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProducts();
    Task<List<Product>?> GetProductsByUserId(int userId);
    Task<List<Product>?> GetBuyHistoryByUserId(int userId);
    Task<Product> GetProductById(int id);
    void AddProduct(Product product);
    Task<int> GetMaxProductId();
    void UpdateProductById(int id, Product product);
    void DeleteProductById(int id);
}
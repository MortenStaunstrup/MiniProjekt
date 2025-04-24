using Core;

namespace MiniProjektGenbrug.Services.Interfaces;

public interface IProductService
{ 
    Task<List<Product>> GetProductsById();
    Task<Product> GetProductById(int id);
    void DeleteProductById(int id);
    void AddProduct(Product product);
    void UpdateProductById(int id, Product product);
}
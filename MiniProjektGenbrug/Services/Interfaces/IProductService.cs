using Core;

namespace MiniProjektGenbrug.Services.Interfaces;

public interface IProductService
{ 
    List<Product> GetProductsById();
    Product GetProductById(int id);
    void DeleteProductById(int id);
    void AddProduct(Product product);
    void UpdateProductById(int id, Product product);
}
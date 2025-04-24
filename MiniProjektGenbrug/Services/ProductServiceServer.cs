using System.Net.Http.Json;
using Core;
using MiniProjektGenbrug.Services.Interfaces;

namespace MiniProjektGenbrug.Services;

public class ProductServiceServer : IProductService
{
    HttpClient _client;
    private string BaseURL = "http://localhost:5189/api/products";

    public ProductServiceServer(HttpClient client)
    {
        _client = client;
    }
    
    
    public async Task<List<Product>> GetProductsById()
    {
        return await _client.GetFromJsonAsync<List<Product>>($"{BaseURL}/getall");
    }

    public Task<Product> GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteProductById(int id)
    {
        throw new NotImplementedException();
    }

    public void AddProduct(Product product)
    {
       _client.PostAsJsonAsync($"{BaseURL}/add", product);
    }

    public void UpdateProductById(int id, Product product)
    {
        throw new NotImplementedException();
    }
}
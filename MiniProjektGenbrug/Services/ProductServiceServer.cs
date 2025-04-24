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

    public async Task<Product> GetProductById(int id)
    {
        return await _client.GetFromJsonAsync<Product>($"{BaseURL}/getbyid/{id}");
    }

    public void DeleteProductById(int id)
    {
        _client.DeleteAsync($"{BaseURL}/delete/{id}");
    }

    public void AddProduct(Product product)
    {
       _client.PostAsJsonAsync($"{BaseURL}/add", product);
    }

    public void UpdateProductById(int id, Product product)
    {
        _client.PutAsJsonAsync($"{BaseURL}/update/{id}", product);
    }
}
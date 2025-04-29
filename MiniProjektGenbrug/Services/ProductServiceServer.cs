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
    
    
    public async Task<List<Product>?> GetAllProducts()
    {
        return await _client.GetFromJsonAsync<List<Product>>($"{BaseURL}/getall");
    }

    public async Task<List<Product>?> GetProductsByUserId(int userId)
    {
        return await _client.GetFromJsonAsync<List<Product>>($"{BaseURL}/getproductsbyuserid/{userId}");
    }

    public async Task<List<Product>?> GetBuyHistoryByUserId(int userId)
    {
        return await _client.GetFromJsonAsync<List<Product>>($"{BaseURL}/gethistorybyid/{userId}");
    }

    public async Task<bool> ExistsInOwnProducts(int productId, int buyerId)
    {
        return await _client.GetFromJsonAsync<bool>($"{BaseURL}/exist/{buyerId}/{productId}");
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _client.GetFromJsonAsync<Product>($"{BaseURL}/getbyid/{id}");
    }

    public void DeleteProductById(int id, int userId)
    {
        _client.DeleteAsync($"{BaseURL}/delete/{id}/{userId}");
    }

    public void AddProduct(Product product, int userId)
    {
       _client.PostAsJsonAsync($"{BaseURL}/add/{userId}", product);
    }

    public void UpdateProductById(int id, int userId, Product product)
    {
        _client.PutAsJsonAsync($"{BaseURL}/update/{id}/{userId}", product);
    }
    
    public void AcceptBid(int productId, int sellerId)
    {
        _client.PutAsJsonAsync($"{BaseURL}/accept/{productId}/{sellerId}", productId);
    }
    
    public void DeclineBid(int productId, int sellerId)
    {
        _client.PutAsJsonAsync($"{BaseURL}/decline/{productId}/{sellerId}", productId);
    }
    
    public void BidOnProduct(int productId, int buyerId)
    {
        _client.PutAsJsonAsync($"{BaseURL}/bid/{productId}/{buyerId}", productId);
    }
}
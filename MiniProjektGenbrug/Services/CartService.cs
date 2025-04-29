using System.Net.Http.Json;
using Blazored.LocalStorage;
using Core;
using MiniProjektGenbrug.Services.Interfaces;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly string BaseURL = "http://localhost:5189/api/users";

    public CartService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }
    
    public async Task AddToCartAsync(int userId, Product product)
    {
        var cart = await GetCartItemsAsync(userId);

        var existingItem = cart.FirstOrDefault(item => item.Product.id == product.id);
        if (existingItem != null)
        {
            existingItem.antal += 1;
        }
        else
        {
            cart.Add(new CartItem
            {
                UserId = userId,
                ProductId = product.id,
                Product = product,
                antal = 1
            });
        }

        await _localStorage.SetItemAsync($"cart_{userId}", cart);
    }

    public async Task AddProductToCartAsync(int userId, Product product)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseURL}/{userId}/addToCart", new { productId = product.id });
        
        if (response.IsSuccessStatusCode)
        {
            var cart = await GetCartItemsAsync(userId);

            var existingItem = cart.FirstOrDefault(item => item.Product.id == product.id);
            if (existingItem != null)
            {
                existingItem.antal += 1;
            }
            else
            {
                cart.Add(new CartItem
                {
                    UserId = userId,
                    ProductId = product.id,
                    Product = product,
                    antal = 1
                });
            }

            await _localStorage.SetItemAsync($"cart_{userId}", cart);
        }
    }

    
    public async Task<List<CartItem>> GetCartItemsAsync(int userId)
    {
        var cart = await _localStorage.GetItemAsync<List<CartItem>>($"cart_{userId}");
        return cart ?? new List<CartItem>();
    }
    
    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        var cart = await GetCartItemsAsync(userId);

        var itemToRemove = cart.FirstOrDefault(item => item.ProductId == productId);
        if (itemToRemove != null)
        {
            cart.Remove(itemToRemove);
            await _localStorage.SetItemAsync($"cart_{userId}", cart);
        }
    }
    
    public async Task ClearCartAsync(int userId)
    {
        await _localStorage.RemoveItemAsync($"cart_{userId}");
    }
}

using Blazored.LocalStorage;
using Core;
using MiniProjektGenbrug.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace MiniProjektGenbrug.Services
{
    public class UserServiceClient : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserServiceClient(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<User?> GetUserLoggedIn()
        {
            return await _localStorage.GetItemAsync<User>("user");
        }
        

        public async Task<User?> GetUserById(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"/api/users/{id}");
            return user;
        }

        public async Task<User?> Login(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/users/login", new
            {
                Username = username,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
                return null;

            var user = await response.Content.ReadFromJsonAsync<User>();
            if (user != null)
                await _localStorage.SetItemAsync("user", user);
            return user;
        }

        public async Task<User?> CreateUserAsync(string username, string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/users/register", new
            {
                Username = username,
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
                return null;

            var newUser = await response.Content.ReadFromJsonAsync<User>();
            if (newUser != null)
                await _localStorage.SetItemAsync("user", newUser);
            return newUser;
        }

        public async Task<List<Product>> GetUserProducts(int userId)
        {
            var products = await _httpClient
                .GetFromJsonAsync<List<Product>>($"/api/users/{userId}/products");
            return products ?? new List<Product>();
        }

        public async Task<List<Product>> GetUserBuyHistory(int userId)
        {
            var history = await _httpClient
                .GetFromJsonAsync<List<Product>>($"/api/users/{userId}/buyhistory");
            return history ?? new List<Product>();
        }
    }
}

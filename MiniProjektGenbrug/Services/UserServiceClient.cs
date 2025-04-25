using Blazored.LocalStorage;
using Core;
using MiniProjektGenbrug.Services.Interfaces;

namespace MiniProjektGenbrug.Services;

public class UserServiceClient : IUserService
{

    private ILocalStorageService localStorage { get; set; }
    public UserServiceClient(ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }
    
    private List<User> users = new List<User>()
    {
        new User
        {
            id = 1,
            Username = "Jonathan",
            Password = "password1",
            Email = "user1@example.com",
            Role = "Seller",
            Products = new List<Product>
            {
                new Product
                {
                    id = 1,
                    Price = 29.99,
                    Productname = "T-Shirt",
                    Description = "Casual cotton t-shirt",
                    Category = "Clothing",
                    Color = "Blue",
                    Picture = null,
                    Size = "M",
                    Status = "Available",
                    BuyerId = null
                },
                new Product
                {
                    id = 2,
                    Price = 59.99,
                    Productname = "Jeans",
                    Description = "Stylish denim jeans",
                    Category = "Clothing",
                    Color = "Black",
                    Picture = null,
                    Size = "L",
                    Status = "Available",
                    BuyerId = null
                },
                new Product
                {
                    id = 3,
                    Price = 199.99,
                    Productname = "Smartphone",
                    Description = "Latest model with high-end features",
                    Category = "Electronics",
                    Color = "Silver",
                    Picture = null,
                    Size = "N/A",
                    Status = "Sold",
                    BuyerId = 2
                }
            },
            BuyHistory = new List<Product>()
            {

            }
        },
        new User
        {
            id = 2,
            Username = "Olga",
            Password = "password2",
            Email = "user2@example.com",
            Role = "Buyer",
            Products = new List<Product>()
            {
                new Product
                {
                    id = 4,
                    Price = 79.99,
                    Productname = "Running Shoes",
                    Description = "Comfortable and durable running shoes",
                    Category = "Footwear",
                    Color = "Red",
                    Picture = null,
                    Size = "10",
                    Status = "Available",
                    BuyerId = null
                },
                new Product
                {
                    id = 5,
                    Price = 499.99,
                    Productname = "Laptop",
                    Description = "Lightweight laptop with powerful performance",
                    Category = "Electronics",
                    Color = "Gray",
                    Picture = null,
                    Size = "N/A",
                    Status = "Pending",
                    BuyerId = null
                }
            },
            BuyHistory = new List<Product>
            {
                new Product
                {
                    id = 3,
                    Price = 199.99,
                    Productname = "Smartphone",
                    Description = "Latest model with high-end features",
                    Category = "Electronics",
                    Color = "Silver",
                    Picture = null,
                    Size = "N/A",
                    Status = "Sold",
                    BuyerId = 2
                }
            }
        }
    };
    
    
    public async Task<User?> GetUserLoggedIn()
    {
        var res = await localStorage.GetItemAsync<User>("user");
        return res;
    }

    public async Task<User?> GetUserById(int id)
    {
        return users.FirstOrDefault((x => x.id == id));
    }

    public async Task<bool> Login(string username, string password)
    {
        var res = users.FirstOrDefault(x => x.Username == username && x.Password == password);
        if (res != null)
        {
            localStorage.SetItemAsync("user", new User { id = res.id, Username = res.Username });
            return true;
        }
        return false;
    }

    public List<Product?> GetUserProducts(int userId)
    {
        return users.FirstOrDefault(x => x.id == userId).Products;
    }

    public List<Product> GetUserBuyHistory(int userId)
    {
        throw new NotImplementedException();
    }
}
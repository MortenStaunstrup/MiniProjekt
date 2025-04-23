using Core;
using MiniProjektGenbrug.Services.Interfaces;

namespace MiniProjektGenbrug.Services;

public class UserServiceClient : IUserService
{
    private List<User> users = new List<User>()
    {
        new User
        {
            id = 1,
            Username = "Jonathan",
            Password = "password1",
            Rmail = "user1@example.com",
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
                    Picture = "tshirt_blue.jpg",
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
                    Picture = "jeans_black.jpg",
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
                    Picture = "smartphone_silver.jpg",
                    Size = "N/A",
                    Status = "Sold",
                    BuyerId = 2
                }
            },
            BuyerHistory = new List<Product>()
            {

            }
        },
        new User
        {
            id = 2,
            Username = "Olga",
            Password = "password2",
            Rmail = "user2@example.com",
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
                    Picture = "shoes_red.jpg",
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
                    Picture = "laptop_gray.jpg",
                    Size = "N/A",
                    Status = "Pending",
                    BuyerId = null
                }
            },
            BuyerHistory = new List<Product>
            {
                new Product
                {
                    id = 3,
                    Price = 199.99,
                    Productname = "Smartphone",
                    Description = "Latest model with high-end features",
                    Category = "Electronics",
                    Color = "Silver",
                    Picture = "smartphone_silver.jpg",
                    Size = "N/A",
                    Status = "Sold",
                    BuyerId = 2
                }
            }
        }
    };
    
    
    public Task<User> GetUserLoggedIn()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetUserProducts(int userId)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetUserBuyHistory(int userId)
    {
        throw new NotImplementedException();
    }
}
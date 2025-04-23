using MiniProjektGenbrug.Services.Interfaces;
using Core;

namespace MiniProjektGenbrug.Services;

public class ProductServiceClient : IProductService
{
    
    List<Product> products = new List<Product>
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
                BuyerId = null
            },
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
        };
    
    public List<Product> GetProductsById()
    {
        return products;
    }

    public Product GetProductById(int id)
    {
        return products.FirstOrDefault(x => x.id == id);
    }

    public void DeleteProductById(int id)
    {
        products.RemoveAll(x => x.id == id);
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void UpdateProductById(int id, Product updatedProduct)
    {
        var product = products.FirstOrDefault(x => x.id == id);
        if (product != null)
        {
            product.Price = updatedProduct.Price;
            product.Productname = updatedProduct.Productname;
            product.Description = updatedProduct.Description;
            product.Category = updatedProduct.Category;
            product.Color = updatedProduct.Color;
            product.Picture = updatedProduct.Picture;
            product.Size = updatedProduct.Size;
            product.Status = updatedProduct.Status;
            product.BuyerId = updatedProduct.BuyerId;
        }
    }
}
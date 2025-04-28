using API.Repositories.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController : ControllerBase
{
    private IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    [Route("getall")]
    public async Task<List<Product>> GetProducts()
    {
        Console.WriteLine("Getting All Products");
        return await _productRepository.GetProducts();
    }

    [HttpGet]
    [Route("getproductsbyid/{userId:int}")]
    public async Task<List<Product>?> GetProductsByUserId(int userId)
    {
        return await _productRepository.GetProductsByUserId(userId);
    }

    [HttpGet]
    [Route("gethistorybyid/{id:int}")]
    public async Task<List<Product>?> GetBuyHistoryByUserId(int userId)
    {
        return await _productRepository.GetBuyHistoryByUserId(userId);
    }

    [HttpGet]
    [Route("getbyid/{id:int}")]
    public async Task<Product> GetProductById(int id)
    {
        return await _productRepository.GetProductById(id);
    }

    [HttpPost]
    [Route("add")]
    public void AddProduct(Product product)
    {
        Console.WriteLine("Product in Controller");
        _productRepository.AddProduct(product);
    }

    [HttpPut]
    [Route("update/{id:int}")]
    public void UpdateProductById(int id, Product product)
    {
        _productRepository.UpdateProductById(id, product);
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    public void DeleteProductById(int id)
    {
        _productRepository.DeleteProductById(id);
    }
    
}
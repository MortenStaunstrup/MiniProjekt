using API.Repositories.Interfaces;
using Core;
using MongoDB.Driver;

namespace API.Repositories;

public class ProductMongoDBRepository : IProductRepository
{
    private string connectionString = "mongodb+srv://mortenstnielsen:hlEgCKJrN89edDQt@clusterfree.a2y2b.mongodb.net/";
    private MongoClient _client;
    private IMongoDatabase database;
    private IMongoCollection<Product> collection;

    public ProductMongoDBRepository()
    {
        _client = new MongoClient(connectionString);
        database = _client.GetDatabase("Genbrug");
        collection = database.GetCollection<Product>("Products");
    }

    public async Task<List<Product>> GetProducts()
    {
        var filter = Builders<Product>.Filter.Empty;
        return await collection.Aggregate().Match(filter).ToListAsync();
    }

    public async Task<Product> GetProductById(int id)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.id, id);
        return await collection.Aggregate().Match(filter).SingleOrDefaultAsync();
    }

    public async Task<int> GetMaxProductId()
    {
        var sort = Builders<Product>.Sort.Descending(x => x.id);
        var maxProduct = await collection
            .Find(Builders<Product>.Filter.Empty)
            .Sort(sort)
            .Limit(1)
            .FirstOrDefaultAsync();
        return maxProduct.id;
    }
    
    public async void AddProduct(Product product)
    {
        product.id = await GetMaxProductId();
        await collection.InsertOneAsync(product);
    }

    public async void UpdateProductById(int id, Product product)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.id, id);
        await collection.ReplaceOneAsync(filter, product);
    }

    public async void DeleteProductById(int id)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.id, id);
        await collection.DeleteOneAsync(filter);
        Console.WriteLine($"Deleting Product: {id}");
    }
}
using System.Buffers.Text;
using API.Repositories.Interfaces;
using Core;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace API.Repositories;

public class ProductMongoDBRepository : IProductRepository
{
    private string connectionString = "mongodb+srv://mortenstnielsen:hlEgCKJrN89edDQt@clusterfree.a2y2b.mongodb.net/";
    private MongoClient _client;
    private IMongoDatabase database;
    private IMongoCollection<Product> collection;
    private IMongoCollection<User> collectionUser;
    private GridFSBucket bucket;

    public ProductMongoDBRepository()
    {
        _client = new MongoClient(connectionString);
        database = _client.GetDatabase("Genbrug");
        collection = database.GetCollection<Product>("Products");
        collectionUser = database.GetCollection<User>("Users");
        bucket = new GridFSBucket(database, new GridFSBucketOptions{ BucketName = "Billeder" });
    }

    public async Task<List<Product>> GetProducts()
    {
        var filter = Builders<Product>.Filter.Empty;
        var result = await collection.Aggregate().Match(filter).ToListAsync();
        if (result.Any())
        {
            foreach (var p in result)
            {
                if(p.PictureId != null)
                    p.Picture = Convert.ToBase64String(await bucket.DownloadAsBytesAsync(p.PictureId));
            }
            return result;
        }
        return result;
    }

    public Task<List<Product>?> GetProductsByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetProductById(int id)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.id, id);
        var result = await collection.Aggregate().Match(filter).SingleOrDefaultAsync();
        if (result != null)
            if(result.PictureId != null)
                result.Picture = Convert.ToBase64String(await bucket.DownloadAsBytesAsync(result.PictureId));
        return result;
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
        product.id = await GetMaxProductId() + 1;
        if (product.Picture != null)
        {
            var picId = await bucket.UploadFromBytesAsync(product.Productname,Convert.FromBase64String(product.Picture));
            product.PictureId = picId;
            product.Picture = null;
        }
        await collection.InsertOneAsync(product);
        Console.WriteLine("Adding product to DB");
    }

    public async void UpdateProductById(int id, Product product)
    {
        var productWId = await GetProductById(id);
        productWId.Productname = product.Productname;
        productWId.Category = product.Category;
        productWId.Price = product.Price;
        productWId.Size = product.Size;
        productWId.Color = product.Color;
        productWId.Description = product.Description;
        productWId.RoomName = product.RoomName;
        productWId.Status = product.Status;
        productWId.BuyerId = product.BuyerId;
        if (product.Picture != null)
        {
            var picId = await bucket.UploadFromBytesAsync(product.Productname,Convert.FromBase64String(product.Picture));
            productWId.PictureId = picId;
            productWId.Picture = null;
        }
        var filter = Builders<Product>.Filter.Eq(x => x.id, id);
        await collection.ReplaceOneAsync(filter, productWId);
    }

    public async void DeleteProductById(int id)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.id, id);
        await collection.DeleteOneAsync(filter);
        Console.WriteLine($"Deleting Product: {id}");
    }
}
using System.Buffers.Text;
using API.Repositories.Interfaces;
using Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

    public async Task<List<Product>?> GetProductsByUserId(int userId)
    {
        var filter = Builders<Core.User>.Filter.Eq(x => x.id, userId);
        var projection = Builders<Core.User>.Projection.Exclude("_id").Include("Products");
        var result = await collectionUser
            .Find(filter)
            .Project(projection)
            .FirstOrDefaultAsync();

        var productList = result["Products"]
            .AsBsonArray
            .Select(product => BsonSerializer.Deserialize<Product>(product.ToBsonDocument()))
            .ToList();
        
        if (result.Any())
        {
            foreach (var p in productList)
            {
                if(p.PictureId != null)
                    p.Picture = Convert.ToBase64String(await bucket.DownloadAsBytesAsync(p.PictureId));
            }
            return productList;
        }
        
        return productList;
    }
    
    public async Task<List<Product>?> GetBuyHistoryByUserId(int userId)
    {
        var filter = Builders<Core.User>.Filter.Eq(x => x.id, userId);
        var projection = Builders<Core.User>.Projection.Exclude("_id").Include("BuyHistory");
        var result = await collectionUser
            .Find(filter)
            .Project(projection)
            .FirstOrDefaultAsync();

        var productList = result["BuyHistory"]
            .AsBsonArray
            .Select(product => BsonSerializer.Deserialize<Product>(product.ToBsonDocument()))
            .ToList();
        
        if (result.Any())
        {
            foreach (var p in productList)
            {
                if(p.PictureId != null)
                    p.Picture = Convert.ToBase64String(await bucket.DownloadAsBytesAsync(p.PictureId));
            }
            return productList;
        }
        
        return productList;
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
        var maxProductId = await collection
            .Find(Builders<Product>.Filter.Empty)
            .Sort(sort)
            .Limit(1)
            .FirstOrDefaultAsync();
        return maxProductId?.id ?? 0;
    }
    
    public async void AddProduct(Product product, int userId)
    {
        product.id = await GetMaxProductId() + 1;
        if (product.Picture != null)
        {
            var picId = await bucket.UploadFromBytesAsync(product.Productname,Convert.FromBase64String(product.Picture));
            product.PictureId = picId;
            product.Picture = null;
        }
        await collection.InsertOneAsync(product);
        
        var filter = Builders<User>.Filter.Eq(x => x.id, userId);
        var update = Builders<User>.Update.Push("Products", product);
        
        await collectionUser.FindOneAndUpdateAsync(filter, update);
        
        Console.WriteLine("Adding product to DB");
    }

    public async void UpdateProductById(int id, Product product, int userId)
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
        
        var filterUser = Builders<User>.Filter;
        var filterUserAndProduct = filterUser.And(filterUser.Eq(x => x.id, userId), filterUser.ElemMatch(x => x.Products, c => c.id == productWId.id));
        
        var update = Builders<User>.Update.Set("Products.$", productWId);
        
        await collectionUser.FindOneAndUpdateAsync(filterUserAndProduct, update);
        
    }

    public async void DeleteProductById(int id, int userId)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.id, id);
        await collection.DeleteOneAsync(filter);
        Console.WriteLine($"Deleting Product: {id}");

        var filterUser = Builders<User>.Filter.Eq(x => x.id, userId);
        var update = Builders<User>.Update.PullFilter("Products", Builders<Product>.Filter.Eq(x => x.id, id));
        
        collectionUser.FindOneAndUpdate(filterUser, update);

    }
}
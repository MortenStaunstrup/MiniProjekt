using API.Repositories.Interfaces;
using Core;
using MongoDB.Driver;

namespace API.Repositories;

public class UserMongoDBRepository : IUserRepository
{
    private string connectionString = "mongodb+srv://mortenstnielsen:hlEgCKJrN89edDQt@clusterfree.a2y2b.mongodb.net/";
    private MongoClient _client;
    private IMongoDatabase database;
    private IMongoCollection<User> collection;

    public UserMongoDBRepository()
    {
        _client = new MongoClient(connectionString);
        database = _client.GetDatabase("Genbrug");
        collection = database.GetCollection<User>("Users");
    }
    
    public async Task<User?> GetUserById(int id)
    {
        var filter = Builders<User>.Filter.Eq(u => u.id, id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
    {
        var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Eq(u => u.Username, username),
            Builders<User>.Filter.Eq(u => u.Password, password)
        );
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<int> GetMaxUserId()
    {
        var sort = Builders<User>.Sort.Descending(x => x.id);
        var maxUser = await collection
            .Find(Builders<User>.Filter.Empty)
            .Sort(sort)
            .Limit(1)
            .FirstOrDefaultAsync();
        return maxUser?.id ?? 0;
    }

    public async Task AddUser(User user)
    {
        await collection.InsertOneAsync(user);
        Console.WriteLine("Adding user to DB");
    }

    public async Task<List<Product>> GetProductsByUserId(int userId)
    {
        var filter = Builders<User>.Filter.Eq(u => u.id, userId);
        var user = await collection.Find(filter).FirstOrDefaultAsync();
        return user?.Products ?? new List<Product>();
    }

    public async Task<List<Product>> GetBuyHistoryByUserId(int userId)
    {
        var filter = Builders<User>.Filter.Eq(u => u.id, userId);
        var user = await collection.Find(filter).FirstOrDefaultAsync();
        return user?.BuyHistory ?? new List<Product>();
    }
}

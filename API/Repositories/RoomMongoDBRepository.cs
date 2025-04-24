using Core;
using MongoDB.Driver;

namespace API.Repositories.Interfaces;

public class RoomMongoDBRepository : IRoomRepository
{
    
    private string connectionString = "mongodb+srv://mortenstnielsen:hlEgCKJrN89edDQt@clusterfree.a2y2b.mongodb.net/";
    private MongoClient _client;
    private IMongoDatabase database;
    private IMongoCollection<Room> collection;

    public RoomMongoDBRepository()
    {
        _client = new MongoClient(connectionString);
        database = _client.GetDatabase("Genbrug");
        collection = database.GetCollection<Room>("Rooms");
    }
    
    public async Task<int> GetMaxRoomId()
    {
        var sort = Builders<Room>.Sort.Descending(x => x.id);
        var maxProduct = await collection
            .Find(Builders<Room>.Filter.Empty)
            .Sort(sort)
            .Limit(1)
            .FirstOrDefaultAsync();
        return maxProduct.id;
    }
    
    public async Task<List<Room>> GetRooms()
    {
        var filter = Builders<Room>.Filter.Empty;
        
        return await collection.Aggregate().Match(filter).ToListAsync();
        
    }

    public async Task<Room> GetRoomById(int id)
    {
        var filter = Builders<Room>.Filter.Eq("_id", id);
        
        return await collection.Aggregate().Match(filter).SingleOrDefaultAsync();
    }

    public async void AddRoom(Room room)
    {
        room.id = await GetMaxRoomId();
        await collection.InsertOneAsync(room);
    }

    public void UpdateRoomById(int id, Room room)
    {
        var filter = Builders<Room>.Filter.Eq("_id", id);
        collection.ReplaceOneAsync(filter, room);
    }

    public void DeleteRoomById(int id)
    {
        var filter = Builders<Room>.Filter.Eq("_id", id);
        collection.DeleteOneAsync(filter);
    }

}
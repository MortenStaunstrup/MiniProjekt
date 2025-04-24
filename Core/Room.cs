using MongoDB.Bson.Serialization.Attributes;

namespace Core;

public class Room
{
    [BsonId]
    public int id { get; set; }
    public string Name { get; set; }
    public DateTime OpeningHour { get; set; }
    public DateTime ClosingHour { get; set; }
    public int Staffing { get; set; }
}
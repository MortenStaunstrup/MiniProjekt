using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Core;

namespace Core
{
    public class CartItem
    { 
        [BsonId]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        
        public int antal;
    }
}
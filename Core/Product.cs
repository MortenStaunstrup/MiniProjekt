using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core;

public class Product
{
    [BsonElement("_id")]
    public int id { get; set; }
    [Required]
    [Range(1, 1000000)]
    public double Price { get; set; }
    [Required]
    [Length(3, 40)]
    public string Productname { get; set; }
    public string? Description { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    [Length(1, 20)]
    public string Color { get; set; }
    public string? Picture { get; set; }
    public ObjectId? PictureId { get; set; }
    public string? PictureSrc { get; set; }
    [Required]
    public string Size { get; set; }
    [Required]
    public string Status { get; set; }
    // Liste af alle bruger Id'er som har anmodet om at købe produktet
    public int? BuyerId { get; set; }
    // FK til Rooms
    [Required]
    public string RoomName { get; set; }
    
}
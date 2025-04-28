using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Core;

public class User
{
    [BsonElement("_id")]
    public int id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    public string Password { get; set; }
    
    [Required]
    public string Role { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();
    public List<Product> BuyHistory { get; set; } = new List<Product>();
}
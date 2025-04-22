namespace Core;

public class Product
{
    public int id { get; set; }
    public double Price { get; set; }
    public string Productname { get; set; }
    public string? Description { get; set; }
    public string Category { get; set; }
    public string Color { get; set; }
    public string? Picture { get; set; }
    public string Size { get; set; }
    public string Status { get; set; }
    // Liste af alle bruger Id'er som har anmodet om at købe produktet
    public int? BuyerId { get; set; }
}
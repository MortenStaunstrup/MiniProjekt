namespace Core;

public class Room
{
    public int id { get; set; }
    public string Name { get; set; }
    public DateTime OpeningHour { get; set; }
    public DateTime ClosingHour { get; set; }
    public int Staffing { get; set; }
    public List<Product> AttachedProducts { get; set; }
}
namespace WebStoreMVC.Models.Seeder;

public class SeederProductModel
{
    public string Name { get; set; } = String.Empty;
    public string Slug { get; set; } = String.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public List<string> Images { get; set; } = null!;

}

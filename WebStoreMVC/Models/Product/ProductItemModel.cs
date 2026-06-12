namespace WebStoreMVC.Models.Product;

public class ProductItemModel
{
    public long Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Slug { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = String.Empty;
    public long CategoryId { get; set; }
    public string CategoryName { get; set; } = String.Empty;
    public string CategorySlug { get; set; } = String.Empty;
    public List<string> Images { get; set; } = null!;
}
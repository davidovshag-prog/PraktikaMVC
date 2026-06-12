namespace WebStoreMVC.Models.Category;

public class CategoryItemModel
{
    public long Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public string Image { get; set; } = String.Empty;

    public string Slug { get; set; } = String.Empty;
}

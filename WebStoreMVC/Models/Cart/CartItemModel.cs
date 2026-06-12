namespace WebStoreMVC.Models.Cart;

public class CartItemModel
{
    public long ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Image { get; set; } = string.Empty;

    public int Quantity { get; set; }
}

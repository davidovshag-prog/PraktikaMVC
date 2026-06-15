namespace WebStoreMVC.Models.Order;

public class CheckoutModel
{
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public List<OrderItemModel> CartItems { get; set; } = [];
    public decimal Total => CartItems.Sum(x => x.Price * x.Quantity);
}

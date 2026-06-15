namespace WebStoreMVC.Models.Order;

public class OrderListItemModel
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatusName { get; set; } = string.Empty;
}
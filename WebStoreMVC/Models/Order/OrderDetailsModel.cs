namespace WebStoreMVC.Models.Order;

public class OrderDetailsModel
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatusName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public List<OrderItemModel> Items { get; set; } = [];
}

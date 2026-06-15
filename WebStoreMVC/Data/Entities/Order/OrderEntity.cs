using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStoreMVC.Data.Entities.Identity;

namespace WebStoreMVC.Data.Entities.Order;

[Table("tblOrders")]
public class OrderEntity : BaseEntity<long>
{
    [ForeignKey("User")]
    public long UserId { get; set; }
    public UserEntity? User { get; set; }

    [ForeignKey("OrderStatus")]
    public long OrderStatusId { get; set; }
    public OrderStatusEntity? OrderStatus { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public decimal TotalAmount { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    public ICollection<OrderItemEntity>? OrderItems { get; set; }
}
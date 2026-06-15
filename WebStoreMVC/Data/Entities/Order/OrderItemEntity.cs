using System.ComponentModel.DataAnnotations.Schema;
using WebStoreMVC.Data.Entities.Catalog;

namespace WebStoreMVC.Data.Entities.Order;

[Table("tblOrderItems")]
public class OrderItemEntity : BaseEntity<long>
{
    [ForeignKey("Order")]
    public long OrderId { get; set; }
    public OrderEntity? Order { get; set; }

    [ForeignKey("Product")]
    public long ProductId { get; set; }
    public ProductEntity? Product { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
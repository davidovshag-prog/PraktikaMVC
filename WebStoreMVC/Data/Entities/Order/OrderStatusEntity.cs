using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStoreMVC.Data.Entities.Order;

[Table("tblOrderStatuses")]
public class OrderStatusEntity : BaseEntity<long>
{
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<OrderEntity>? Orders { get; set; }
}

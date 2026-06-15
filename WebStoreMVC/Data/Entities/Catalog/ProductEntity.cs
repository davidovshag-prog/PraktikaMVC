using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStoreMVC.Data.Entities.Order;

namespace WebStoreMVC.Data.Entities.Catalog;

[Table("tblProducts")]
public class ProductEntity : BaseEntity<long>
{
    [StringLength(250)]
    public string Name { get; set; } = String.Empty;

    [StringLength(250)]
    public string Slug { get; set; } = String.Empty;

    public decimal Price { get; set; }

    [StringLength(1000)]
    public string Description { get; set; } = String.Empty;

    [ForeignKey("Category")]
    public long CategoryId { get; set; }

    public CategoryEntity? Category { get; set; }
    public ICollection<ProductImageEntity>? ProductImages { get; set; }
    public ICollection<OrderItemEntity>? OrderItems { get; set; }
}

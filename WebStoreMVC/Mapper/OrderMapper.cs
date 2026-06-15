namespace WebStoreMVC.Mapper;

using Riok.Mapperly.Abstractions;
using WebStoreMVC.Data.Entities.Order;
using WebStoreMVC.Models.Order;

[Mapper]
public partial class OrderMapper
{
    [MapProperty(nameof(OrderEntity.OrderStatus.Name), nameof(OrderListItemModel.OrderStatusName))]
    public partial OrderListItemModel OrderEntityToListItemModel(OrderEntity entity);

    public partial List<OrderListItemModel> ListOrderEntityToListItemModels(IEnumerable<OrderEntity> entities);

    [MapProperty(nameof(OrderEntity.OrderStatus.Name), nameof(OrderDetailsModel.OrderStatusName))]
    [MapProperty(nameof(OrderEntity.OrderItems), nameof(OrderDetailsModel.Items))]
    public partial OrderDetailsModel OrderEntityToDetailsModel(OrderEntity entity);

    [MapProperty(nameof(OrderItemEntity.Product.Name), nameof(OrderItemModel.Name))]
    private partial OrderItemModel OrderItemEntityToItemModel(OrderItemEntity entity);

    private List<OrderItemModel> MapOrderItems(ICollection<OrderItemEntity>? items)
    {
        return items?
            .Select(x =>
            {
                var m = OrderItemEntityToItemModel(x);
                m.Image = x.Product?.ProductImages?
                    .OrderBy(i => i.Priority)
                    .FirstOrDefault()?.Name ?? "no-image.webp";
                return m;
            })
            .ToList()
            ?? [];
    }
}
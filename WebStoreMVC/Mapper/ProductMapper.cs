namespace WebStoreMVC.Mapper;

using Riok.Mapperly.Abstractions;
using WebStoreMVC.Data.Entities.Catalog;
using WebStoreMVC.Models.Product;
using WebStoreMVC.Models.Seeder;

[Mapper]
public partial class ProductMapper
{
    [MapperIgnoreTarget(nameof(ProductEntity.Id))]
    [MapperIgnoreTarget(nameof(ProductEntity.Category))]
    [MapperIgnoreTarget(nameof(ProductEntity.ProductImages))]
    public partial ProductEntity SeederProductModelToProductEntity(SeederProductModel model);

    [MapProperty(nameof(ProductEntity.Category.Name), nameof(ProductItemModel.CategoryName))]
    [MapProperty(nameof(ProductEntity.Category.Slug), nameof(ProductItemModel.CategorySlug))]
    [MapProperty(nameof(ProductEntity.ProductImages), nameof(ProductItemModel.Images))]
    public partial ProductItemModel ProductEntityToProductItemModel(ProductEntity entity);

    public partial List<ProductItemModel> ListProductEntityToItemModels(IEnumerable<ProductEntity> entities);

    private List<string> MapImages(ICollection<ProductImageEntity>? images)
    {
        return images?
            .OrderBy(x => x.Priority)
            .Select(x => x.Name)
            .ToList()
            ?? [];
    }
}

using Riok.Mapperly.Abstractions;
using WebStoreMVC.Data.Entities.Catalog;
using WebStoreMVC.Data.Entities.Identity;
using WebStoreMVC.Models.Seeder;

namespace WebStoreMVC.Mapper;

[Mapper]
public partial class CategoryMapper
{
    [MapperIgnoreTarget(nameof(UserEntity.Image))]
    public partial CategoryEntity SeederCategoryToCategory(SeederCategoryModel seedCategory);
}

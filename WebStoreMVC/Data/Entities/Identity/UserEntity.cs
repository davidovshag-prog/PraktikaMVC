using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebStoreMVC.Data.Entities.Order;

namespace WebStoreMVC.Data.Entities.Identity;

public class UserEntity : IdentityUser<long>
{
    [StringLength(100)]
    public string? FirstName { get; set; }
    [StringLength(100)]
    public string? LastName { get; set; }
    [StringLength(100)]
    public string? Image { get; set; }
    public ICollection<UserRoleEntity>? UserRoles { get; set; }
    public ICollection<OrderEntity>? Orders { get; set; }
}

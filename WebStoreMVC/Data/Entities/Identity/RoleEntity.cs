using Microsoft.AspNetCore.Identity;

namespace WebStoreMVC.Data.Entities.Identity;

public class RoleEntity : IdentityRole<long>
{
    public ICollection<UserRoleEntity>? UserRoles { get; set; }
}

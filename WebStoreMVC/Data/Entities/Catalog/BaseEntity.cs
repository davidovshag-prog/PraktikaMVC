using System.ComponentModel.DataAnnotations;

namespace WebStoreMVC.Data.Entities;

public abstract class BaseEntity<T> : IEntity<T>
{
    [Key]
    public T Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
}

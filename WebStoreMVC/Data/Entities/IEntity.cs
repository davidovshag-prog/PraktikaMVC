namespace WebStoreMVC.Data.Entities;

public interface IEntity<T>
{
    T Id { get; set; }
    bool IsDeleted { get; set; }
    DateTime DateCreated { get; set; }
}


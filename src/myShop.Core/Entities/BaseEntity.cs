namespace myShop.Core.Entites;

public abstract class BaseEntity
{
    [Key]
    public virtual int Id { get; protected set; }
}
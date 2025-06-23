namespace myShop.Core.Entites;

public class Brand(string Name) : BaseEntity, IAggregateRoot
{
    [Required]
    public string? Name { get; private set; } = Name;
}
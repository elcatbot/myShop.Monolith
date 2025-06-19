namespace myShop.Core.Entites;

public class Brand(string Name) : BaseEntity
{
    [Required]
    public string? Name { get; private set; } = Name;
}
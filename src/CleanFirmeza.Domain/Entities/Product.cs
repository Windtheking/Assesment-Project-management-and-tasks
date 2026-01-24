namespace CleanFirmeza.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public  string? Description { get; set; }
    public decimal UnitCost { get; set; }

    public bool IsActive { get; set; } = true;
}
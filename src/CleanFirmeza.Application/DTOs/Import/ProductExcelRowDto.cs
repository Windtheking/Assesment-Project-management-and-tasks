namespace CleanFirmeza.Application.DTOs.Import;


public class ProductExcelRowDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal UnitCost { get; set; }
}
using System.ComponentModel.DataAnnotations;
namespace CleanFirmeza.Application.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Name is Mandatory")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Name must have a minimun of 3 characters")] 
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Description is mandatory")]
    [StringLength(255, ErrorMessage = "Description must not exceed 255 characters")]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "Unitary cost is mandatory")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Unitary cost must bwe higher than $0.00")]
    public decimal UnitCost { get; set; }
    
}


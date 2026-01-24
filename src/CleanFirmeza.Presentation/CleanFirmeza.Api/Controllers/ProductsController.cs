namespace CleanFirmeza.Api.Controllers;


using CleanFirmeza.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("UploadExcel")]
    public async Task<IActionResult> UploadExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Archivo no válido");

        using Stream excelStream = file.OpenReadStream();

        var inserted = await _productService.ImportFromExcelAsync(excelStream);

        return Ok(inserted);
    }
}
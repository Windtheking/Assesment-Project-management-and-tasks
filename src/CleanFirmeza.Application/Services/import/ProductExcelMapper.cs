using CleanFirmeza.Application.DTOs.Import;
using CleanFirmeza.Application.Interfaces;
using CleanFirmeza.Application.Interfaces.Import;
using ClosedXML.Excel;

/*
 * This process maps the entire excle sheet to have all the information analized and if it follows the
 * prestablished structure then it will be accepted and let through through the repo all the way to the
 * database
 */
namespace CleanFirmeza.Application.Services.import;

using ClosedXML.Excel;

public class ProductExcelMapper : IProductExcelMapper
{
    private readonly IProductService _productService;
    
    public IReadOnlyDictionary<int, string> ExpectedColumns { get; } =
        new Dictionary<int, string>
        {
            { 1, "Name" },
            { 2, "Description" },
            { 3, "UnitCost" }
        };

    public ProductExcelRowDto MapRow(IXLRow row, int rowNumber)
    {
        try
        {
            return new ProductExcelRowDto
            {
                Name = row.Cell(1).GetString(),
                Description = row.Cell(2).GetString(),
                UnitCost = row.Cell(3).GetValue<decimal>()
            };
        }
        catch
        {
            throw new Exception($"Format error on row: {rowNumber}");
        }
    }

    public async Task<List<ProductExcelRowDto>> ImportFromExcelAsync(Stream file)
    {
        var result = new List<ProductExcelRowDto>();

        using var workbook = new XLWorkbook(file);
        var worksheet = workbook.Worksheets.FirstOrDefault();

        if (worksheet == null)
            return result;

        int rowNumber = 2; //For this to work headers must be on line 1

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            var dto = MapRow(row, rowNumber);
            result.Add(dto);
            rowNumber++;
        }

        return result;
    }

}


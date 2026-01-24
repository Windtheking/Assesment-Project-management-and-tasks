namespace CleanFirmeza.Application.Interfaces.Import;
using CleanFirmeza.Application.DTOs.Import;
using ClosedXML.Excel;

public interface IProductExcelMapper
{
    IReadOnlyDictionary<int, string> ExpectedColumns { get; }
    ProductExcelRowDto MapRow(IXLRow row, int rowNumber);
    
    Task<List<ProductExcelRowDto>> ImportFromExcelAsync(Stream file);
}
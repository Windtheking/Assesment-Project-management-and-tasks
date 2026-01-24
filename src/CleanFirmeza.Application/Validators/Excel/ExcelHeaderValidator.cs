using ClosedXML.Excel;

namespace CleanFirmeza.Application.Validators.Excel;

public static class ExcelHeaderValidator
{
    public static void Validate(
        IXLWorksheet sheet,
        IReadOnlyDictionary<int, string> expectedColumns)
    {
        var headerRow = sheet.Row(1);

        foreach (var (index, expected) in expectedColumns)
        {
            var actual = headerRow.Cell(index).GetString().Trim();

            if (!string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    $"Invalid column. Expected '{expected}' in column {index}");
            }
        }
    }
}
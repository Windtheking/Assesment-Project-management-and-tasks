
using CleanFirmeza.Application.Common;
using CleanFirmeza.Application.DTOs;
using CleanFirmeza.Application.Interfaces;
using CleanFirmeza.Domain.Entities;
using CleanFirmeza.Domain.Interface;
using ExcelPackage = OfficeOpenXml.ExcelPackage;


namespace CleanFirmeza.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private const int PageSize = 10;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Product>> GetAllAsync()
        => await _repo.GetAllAsync();

    public async Task<PagedResult<ProductDto>> GetPagedAsync(int page, string? searchTerm = null)
    {
        // Validate page number
        if (page < 1)
            page = 1;

        // Calculate skip
        int skip = (page - 1) * PageSize;

        // Get total count and items with search filter
        var totalCount = await _repo.GetTotalCountAsync(searchTerm);
        var items = await _repo.GetPagedAsync(skip, PageSize, searchTerm);
        var dtoItems = items.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            UnitCost = p.UnitCost
        }).ToList();


        return new PagedResult<ProductDto>
        {
            Items = dtoItems,
            CurrentPage = page,
            PageSize = PageSize,
            TotalCount = totalCount,
            SearchTerm = searchTerm
        };
    }

    public async Task<Product?> GetByIdAsync(Guid id)
        => await _repo.GetByIdAsync(id);

    public async Task CreateAsync(Product product)
    {
        product.Id = Guid.NewGuid();
        await _repo.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        await _repo.UpdateAsync(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) return;

        await _repo.DeleteAsync(product);
    }

    public async Task<int> ImportFromExcelAsync(Stream excelStream)
    {
        // EPPlus 8 – licencia NO comercial
        ExcelPackage.License.SetNonCommercialOrganization("CleanFirmeza");

        using var package = new ExcelPackage(excelStream);
        var worksheet = package.Workbook.Worksheets.FirstOrDefault();

        if (worksheet == null || worksheet.Dimension == null)
            return 0;

        int inserted = 0;

        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
        {
            var name = worksheet.Cells[row, 1].GetValue<string>();
            var description = worksheet.Cells[row, 2].GetValue<string>();
            var unitCost = worksheet.Cells[row, 3].GetValue<decimal>();

            // Validación mínima
            if (string.IsNullOrWhiteSpace(name) || unitCost <= 0)
                continue;

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                UnitCost = unitCost
            };

            await _repo.AddAsync(product);
            inserted++;
        }

        return inserted;
    }
}


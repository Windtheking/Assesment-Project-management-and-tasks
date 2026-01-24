using CleanFirmeza.Application.Common;
using CleanFirmeza.Application.DTOs;
using CleanFirmeza.Application.DTOs.Import;
using CleanFirmeza.Application.Interfaces;
using CleanFirmeza.Application.Interfaces.Import;
using CleanFirmeza.Domain.Entities;
using CleanFirmeza.Domain.Interface;

namespace CleanFirmeza.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private const int PageSize = 10;
    private readonly IProductExcelMapper _excelMapper;
    
    public ProductService(IProductRepository repo,  IProductExcelMapper excelMapper)
    {
        _repo = repo;
        _excelMapper = excelMapper;
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

    public async Task<int> ImportFromExcelAsync(Stream file)
    {
        try
        {
            var rows = await _excelMapper.ImportFromExcelAsync(file);
            
            
            foreach (var row in rows)
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = row.Name,
                    Description = row.Description,
                    UnitCost = row.UnitCost
                };
                await _repo.AddAsync(product);
                
            }

            return rows.Count;
        }
        catch (InvalidOperationException ex)
        {
            throw new ApplicationException(ex.Message);   
        }
    }
}
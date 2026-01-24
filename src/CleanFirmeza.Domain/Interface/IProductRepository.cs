    using CleanFirmeza.Domain.Entities;

    namespace CleanFirmeza.Domain.Interface;

    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<bool> ExistsAsync(Guid id);
        Task<List<Product>> GetPagedAsync(int skip, int take, string? searchTerm = null); // UPDATED
        Task<int> GetTotalCountAsync(string? searchTerm = null); // UPDATED
    }
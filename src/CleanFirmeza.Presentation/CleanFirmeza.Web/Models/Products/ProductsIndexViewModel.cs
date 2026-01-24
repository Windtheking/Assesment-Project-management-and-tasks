using CleanFirmeza.Application.DTOs;

/*
View model to help have pagination, this model helps the view 
respect the clean architecture but still have pagination and 
search bar searching without breaking the project
*/

namespace CleanFirmeza.Web.Models.Products
{
    public class ProductsIndexViewModel
    {
        public List<ProductDto> Products { get; set; } = new();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public string? SearchTerm { get; set; }

        //This helps the view be aware where it is and where it is not
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
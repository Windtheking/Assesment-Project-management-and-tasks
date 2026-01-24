using CleanFirmeza.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using CleanFirmeza.Application.Interfaces;
using CleanFirmeza.Domain.Entities;
using CleanFirmeza.Web.Models.Products;

namespace CleanFirmeza.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        public async Task<IActionResult> Index(int page = 1, string? searchTerm = null)
        {
            var pagedResult = await _productService.GetPagedAsync(page, searchTerm);

            var viewModel = new ProductsIndexViewModel
            {
                Products = pagedResult.Items,
                CurrentPage = pagedResult.CurrentPage,
                TotalPages = (int)Math.Ceiling(pagedResult.TotalCount / (double)pagedResult.PageSize),
                SearchTerm = pagedResult.SearchTerm
            };

            return View(viewModel);
        }

        // =========================
        // DETAILS
        // =========================
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // =========================
        // CREATE
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var product = new Product
            {
                Name = dto.Name, Description = dto.Description, UnitCost = dto.UnitCost, IsActive = true
            };

            await _productService.CreateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // BULK CREATE
        // =========================
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { success = false, message = "Not valid file type" });

            try
            {
                using var stream = file.OpenReadStream();
                var inserted = await _productService.ImportFromExcelAsync(stream);

                return Ok(new
                {
                    success = true,
                    message = $"{inserted} products inserted correctly"
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Unexpected error while loading the file, please reload and retry"
                });
            }
        }


        
        
        // =========================
        // EDIT
        // =========================
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            var dto = new ProductDto
            {
                Id = product.Id, Name = product.Name, Description = product.Description, UnitCost = product.UnitCost
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductDto dto)
        {
            if (id != dto.Id) return NotFound();

            if (!ModelState.IsValid) return View(dto);

            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.UnitCost = dto.UnitCost;

            await _productService.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE
        // =========================
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
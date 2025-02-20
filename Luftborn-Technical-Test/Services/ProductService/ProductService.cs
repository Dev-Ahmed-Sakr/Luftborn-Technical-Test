using Luftborn_Technical_Test.Data;
using Luftborn_Technical_Test.Models;
using Luftborn_Technical_Test.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Luftborn_Technical_Test.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await _context.Products
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToListAsync();
        }

        public async Task<ProductViewModel?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public async Task<ProductViewModel> CreateAsync(ProductViewModel productViewModel)
        {
            var product = new Product
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            productViewModel.Id = product.Id;
            return productViewModel;
        }

        public async Task<bool> UpdateAsync(int id, ProductViewModel productViewModel)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.Name = productViewModel.Name;
            product.Price = productViewModel.Price;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

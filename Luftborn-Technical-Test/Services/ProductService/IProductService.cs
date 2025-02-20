using Luftborn_Technical_Test.ViewModels;

namespace Luftborn_Technical_Test.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel?> GetByIdAsync(int id);
        Task<ProductViewModel> CreateAsync(ProductViewModel productViewModel);
        Task<bool> UpdateAsync(int id, ProductViewModel productViewModel);
        Task<bool> DeleteAsync(int id);
    }
}

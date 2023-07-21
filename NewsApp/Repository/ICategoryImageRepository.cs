using NewsApp.Models;

namespace NewsApp.Repository
{
    public interface ICategoryImageRepository
    {
        Task<List<ImageCategory>> GetImageCategories();
        Task<ImageCategory> GetImageCategoryById(int id);
        Task<ImageCategory> CreateImageCategory(ImageCategory imageCategory);
        Task<ImageCategory> UpdateImageCategory(ImageCategory imageCategory);
        Task<ImageCategory> DeleteImageCategory(ImageCategory imageCategory);

    }
}

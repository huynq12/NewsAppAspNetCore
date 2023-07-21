using NewsApp.Models;

namespace NewsApp.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<List<Category>> GetCategoriesByPostId(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(Category category);


    }
}

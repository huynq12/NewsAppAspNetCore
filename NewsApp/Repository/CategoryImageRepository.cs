using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Repository
{
    public class CategoryImageRepository : ICategoryImageRepository
    {
        private readonly DataContext _context;

        public CategoryImageRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<ImageCategory> CreateImageCategory(ImageCategory imageCategory)
        {
            _context.ImageCategories.Add(imageCategory);
            await _context.SaveChangesAsync();
            return imageCategory;
        }

        public async Task<ImageCategory> DeleteImageCategory(ImageCategory imageCategory)
        {
            _context.ImageCategories.Remove(imageCategory);
            await _context.SaveChangesAsync();
            return imageCategory;
        }

        public async Task<List<ImageCategory>> GetImageCategories()
        {
            return await _context.ImageCategories.ToListAsync();
        }

        public async Task<ImageCategory> GetImageCategoryById(int id)
        {
            return await _context.ImageCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ImageCategory> UpdateImageCategory(ImageCategory imageCategory)
        {
            _context.ImageCategories.Update(imageCategory);
            await _context.SaveChangesAsync();
            return imageCategory;
        }
    }
}

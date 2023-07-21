using NewsApp.Models;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;


namespace NewsApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesByPostId(int postId)
        {
            var query = from c in _context.Categories
                        join pc in _context.PostCategories on c.Id equals pc.CategoryId
                        where pc.PostId == postId
                        select c;
            return await query.ToListAsync();


        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}

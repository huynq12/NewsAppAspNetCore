using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.ViewModels;
using NewsApp.ViewModels.Seedwork;

namespace NewsApp.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<Post> CreatePost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<List<PostCategory>> DelPostCatgories(List<PostCategory> postCategories)
        {
            _context.PostCategories.RemoveRange(postCategories);
            await _context.SaveChangesAsync();
            return postCategories;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts.Include(x => x.PostCategories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<Post>> GetPosts(Filter filter)
        {
            const int pageSize = 5;
            filter.PageSize = pageSize;

            var query = _context.Posts.AsQueryable();

            query = query.OrderByDescending(x => x.CreatedAt);

            if (!filter.CategoryIds.IsNullOrEmpty())
            {
                query = query.Where(x => x.PostCategories.Any(pc => filter.CategoryIds.Contains(pc.CategoryId)));
            }

            //paging

            int recsCount = await query.CountAsync();

            int recSkip = (filter.PageNumber - 1) * filter.PageSize;

            var data = await query.Skip(recSkip).Take(filter.PageSize).ToListAsync();

            return new PagedList<Post>(data, recsCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<Post> UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}

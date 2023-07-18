using NewsApp.Models;
using NewsApp.ViewModels;
using NewsApp.ViewModels.Seedwork;

namespace NewsApp.Repository
{
    public interface IPostRepository
    {
        Task<PagedList<Post>> GetPosts(Filter filter);
        Task<List<Post>> GetAllPosts();
        Task<Post> GetPostById (int id);
        Task<Post> CreatePost(Post post);
        Task<Post> UpdatePost(Post post);
        Task<Post> DeletePost(Post post);
        Task<List<PostCategory>> DelPostCatgories(List<PostCategory> postCategories);
    }
}

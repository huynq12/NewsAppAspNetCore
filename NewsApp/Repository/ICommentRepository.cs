using NewsApp.Models;

namespace NewsApp.Repository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetComments(int postId);
        Task<Comment> GetCommentById(int id);
        Task<Comment> CreateComment(Comment comment);
        Task<Comment> UpdateComment(Comment comment);
        Task<Comment> DeleteComment(Comment comment);
    }
}

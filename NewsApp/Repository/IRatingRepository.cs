
using NewsApp.Models;

namespace NewsApp.Repository
{
    public interface IRatingRepository
    {
        Task<List<Rating>> GetRatings();
        Task<Rating?> GetRatingById(int? id);
        Task<List<Rating>> GetRatingsByPostId(int postId);
        Task<Rating> GetRatingByUserId(string userId);
        Task<Rating> CreateRating(Rating rating);
        Task<Rating> Update(Rating rating);
        Task<Rating> Delete(Rating rating);
        Task<bool> HasRating(int postId, string userId);


    }
}

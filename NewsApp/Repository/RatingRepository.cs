using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DataContext _context;

        public RatingRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<Rating> CreateRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> Delete(Rating rating)
        {
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating?> GetRatingById(int? id)
        {
            return await _context.Ratings.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Rating>> GetRatingsByPostId(int postId)
        {
            return await _context.Ratings
                .Include(x => x.User)
                .Where(x => x.PostId == postId)
                .OrderByDescending(x => x.Rate)
                .ToListAsync();
        }

        public async Task<List<Rating>> GetRatings()
        {
            return await _context.Ratings.OrderByDescending(x => x.Rate).ToListAsync();
        }

        public async Task<Rating> Update(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<bool> HasRating(int postId, string userId)
        {
            var isRated = _context.Ratings.Any(c => c.PostId == postId && c.UserId == userId);
            return isRated;
        }

        public async Task<Rating> GetRatingByUserId(string userId)
        {
            return await _context.Ratings.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}

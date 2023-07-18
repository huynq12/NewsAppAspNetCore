
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.ViewModels.Seedwork;

namespace NewsApp.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;

        public ReportRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<ReportedList<Category,int>> GetTopCategories()
        {
            var topCategories = _context.PostCategories
                         .GroupBy(pc => pc.CategoryId) // Nhóm theo CategoryId
                         .Select(g => new { CategoryId = g.Key, Count = g.Count() }) // Chọn CategoryId và đếm số post trong mỗi category
                         .OrderByDescending(x => x.Count) 
                         .Take(10) 
                         .Join(_context.Categories, pc => pc.CategoryId, c => c.Id, (pc, c) => new { Category = c, Count = pc.Count }); // Kết hợp với bảng danh mục để lấy danh mục thay vì CategoryId

            List<Category> list = new List<Category>();
            List<int> count = new List<int>();
            foreach(var category in topCategories)
            {
                list.Add(category.Category);
                count.Add(category.Count);
            }
            return new ReportedList<Category,int>(list, count);

        }

        public async Task<ReportedList<Post,double>> GetTopPostsRate()
        {
            var topPosts =  _context.Comments
                .GroupBy(c => c.PostId)
                .Select(g => new
                {
                    PostId = g.Key,
                    AverageRating = g.Average(c => c.Rate)
                })
                .OrderByDescending(g => g.AverageRating)
                .Take(10)
                .Join(_context.Posts, cm => cm.PostId, p => p.Id, (cm, p) => new { Post = p, AverageRate = cm.AverageRating });
                

            List<Post> list = new List<Post>();
            List<double> averageRatingList = new List<double>();
            
            foreach (var post in topPosts)
            {
                list.Add(post.Post);
                averageRatingList.Add(post.AverageRate);
            }

            return new ReportedList<Post,double>(list, averageRatingList);

        }

        public async Task<ReportedList<Post,int>> GetTopPostsReview()
        {
            var topPosts = _context.Comments
                .GroupBy(c => c.PostId)
                .Select(g => new
                {
                    PostId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .Take(10)
                .Join(_context.Posts, cm => cm.PostId, p => p.Id, (cm, p) => new { Post = p, Count = cm.Count });

            List<Post> list = new List<Post>();
            List<int> count = new List<int>();

            foreach (var post in topPosts)
            {
                list.Add(post.Post);
                count.Add(post.Count);
            }

            return new ReportedList<Post,int>(list, count);


        }
    }
}

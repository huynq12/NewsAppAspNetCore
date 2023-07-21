using NewsApp.Models;
using NewsApp.ViewModels.Seedwork;

namespace NewsApp.Repository
{
    public interface IReportRepository
    {
        Task<ReportedList<Post, int>> GetTopPostsReview();
        Task<ReportedList<Post, double>> GetTopPostsRate();
        Task<ReportedList<Category, int>> GetTopCategories();

    }
}

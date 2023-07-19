using Microsoft.AspNetCore.Mvc;
using NewsApp.Repository;

namespace NewsApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TopCategories()
        {
            var result = await _reportRepository.GetTopCategories();
            return View(result);
        }

        public async Task<IActionResult> TopPostRate()
        {
            var result = await _reportRepository.GetTopPostsRate();
            return View(result);
        }

        public async Task<IActionResult> TopPostReview()
		{
			var result = await _reportRepository.GetTopPostsReview();
			return View(result);
		}
	}
}

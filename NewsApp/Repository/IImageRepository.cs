using NewsApp.Models;
using NewsApp.ViewModels;
using NewsApp.ViewModels.Seedwork;

namespace NewsApp.Repository
{
    public interface IImageRepository
    {
        Task<PagedList<Image>> GetImages(Filter filter);
        Task<Image> GetImageById(int id);
        Task<Image> CreateImage(Image image);
        Task<Image> UpdateImage(Image image);
        Task<Image> DeleteImage(Image image);
    }
}

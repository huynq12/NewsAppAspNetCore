using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.ViewModels;
using NewsApp.ViewModels.Seedwork;

namespace NewsApp.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;

        public ImageRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<Image> CreateImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<Image> DeleteImage(Image image)
        {
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<Image?> GetImageById(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<Image>> GetImages(Filter filter)
        {
            const int pageSize = 5;
            filter.PageSize = pageSize;

            //filter
            var query = _context.Images.OrderByDescending(x => x.UploadTime).AsQueryable();

            if (!filter.CategoryIds.IsNullOrEmpty())
            {
                query = query.Where(x => x.CategoryImages.Any(pc => filter.CategoryIds.Contains(pc.CatImageId)));
            }

            if (filter.FilterOrder.HasValue)
            {
                switch ((int)filter.FilterOrder.Value)
                {
                    case 0://nameasc
                        query = query.OrderBy(x => x.MyImage);
                        break;
                    case 1://namedesc
                        query = query.OrderByDescending(x => x.MyImage);
                        break;
                    case 2://old
                        query = query.OrderBy(x => x.UploadTime);
                        break;
                    case 3://new
                        query = query.OrderByDescending(x => x.UploadTime);
                        break;
                    default://new
                        break;
                }
            }
            //paging

            int recsCount = await query.CountAsync();
            int recSkip = (filter.PageNumber - 1) * filter.PageSize;

            var data = await query.Skip(recSkip).Take(filter.PageSize).ToListAsync();
            return new PagedList<Image>(data, recsCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<Image> UpdateImage(Image image)
        {
            _context.Images.Update(image);
            await _context.SaveChangesAsync();
            return image;
        }
    }
}

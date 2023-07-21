using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.ViewModels
{
    public class ImageDto
    {
        public int Id { get; set; } 
        public IFormFile MyImage { get; set; }
        public string? ImageDescription { get; set; }
        public DateTime UploadTime { get; set; }
        public List<SelectListItem> SelectListImgCats { get; set; }
        [Display(Name = "ImageCategories")]
        public int[]? ImgCategoryIds { get; set; }
    }
}

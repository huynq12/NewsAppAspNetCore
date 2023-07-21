using System.IO;
using System.Net;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using NewsApp.Models;
using NewsApp.Repository;
using NewsApp.ViewModels;
using NewsApp.ViewModels.Seedwork;
using static System.Net.WebRequestMethods;

namespace NewsApp.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageRepository _imageRepository;
        private readonly ICategoryImageRepository _catImageRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImageController(IImageRepository imageRepository,
            ICategoryImageRepository categoryImageRepository,
            IWebHostEnvironment hostingEnvironment
            )
        {
            _imageRepository = imageRepository;
            _catImageRepository = categoryImageRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index(Filter filter,int pg=1 )
        {
            filter.PageNumber = pg;
            var listCategory = await _catImageRepository.GetImageCategories();

            filter.SelectListCats = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.Filter = filter;
            if(filter.CategoryIds != null)
            {
                ViewBag.ImgCatId = filter.CategoryIds.ToList().FirstOrDefault();
            }

            PagedList<Models.Image> result = await _imageRepository.GetImages(filter);

            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var listImgCategory = await _catImageRepository.GetImageCategories();
            ImageDto model = new ImageDto();

            model.SelectListImgCats = listImgCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return View(model);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(ImageDto model)
        {
            UploadImageRequest request = UploadFile(model.MyImage);

            List<CategoryImages> imgCategories = new List<CategoryImages>();

            if (model.ImgCategoryIds.Length > 0)
            {
                foreach (var imgCatId in model.ImgCategoryIds)
                {
                    imgCategories.Add(new CategoryImages
                    {
                        CatImageId = imgCatId,
                        ImageId = model.Id
                    });
                }
                await _imageRepository.CreateImage(new Models.Image
                {
                    ImageDescription = model.ImageDescription,
                    UploadTime = DateTime.Now,
                    MyImage = request.FileName,
                    ImageSize = request.ImageSize,
                    CategoryImages = imgCategories
                });
            }
            return RedirectToAction("Index", "Image");
           
        }
        

        private UploadImageRequest UploadFile(IFormFile myFile)
        {
            UploadImageRequest request = new UploadImageRequest();
            
            if(myFile != null)
            {
                string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                request.FileName = Guid.NewGuid().ToString() + "-" + myFile.FileName;
                request.FilePath = Path.Combine(uploadDir, request.FileName);
                using (var fileStream = new FileStream(request.FilePath, FileMode.Create))
                {
                    myFile.CopyTo(fileStream);
                }
                var image = System.Drawing.Image.FromFile(request.FilePath);

                int width = image.Width;
                int height = image.Height;
                request.ImageSize = width.ToString() + "x" + height.ToString();

            }

            return request;
        }
        

        public async Task<IActionResult> Edit(int id)
        {
            var existingImage = await _imageRepository.GetImageById(id);

            if (existingImage == null)
                return BadRequest();

            return View(existingImage);
        }

        public async Task<IActionResult> Details(int id)
        {
            var existingImage = await _imageRepository.GetImageById(id);

            if (existingImage == null)
                return BadRequest();

            return View(existingImage);
        }

        /* [HttpPost]
         public async Task<IActionResult> Edit(int id,Image model)
         {
             var existingImage = await _imageRepository.GetImageById(id);

             if (existingImage == null)
                 return BadRequest();    


             existingImage.ImageDescription = model.ImageDescription;

             var newFileName = GetUniqueFileName(model.MyImage);
             var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", $"{existingImage.ImageCaption}");

             if (System.IO.File.Exists(path))
             {
                 System.IO.File.Delete(path);
             }
             existingImage.ImageCaption = newFileName;

             await _imageRepository.UpdateImage(existingImage);
             return RedirectToAction("Index","Image");

         }*/
        public async Task<IActionResult> Delete(int id)
        {
            var existingImage = await _imageRepository.GetImageById(id);

            if (existingImage == null)
                return BadRequest();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", $"{existingImage.MyImage}");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            await _imageRepository.DeleteImage(existingImage);

            return RedirectToAction("Index", "Image");


        }



    }
}

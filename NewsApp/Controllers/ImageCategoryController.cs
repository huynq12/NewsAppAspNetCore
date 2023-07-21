using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using NewsApp.Repository;
using System.Net.WebSockets;

namespace NewsApp.Controllers
{
    public class ImageCategoryController : Controller
    {
        private readonly ICategoryImageRepository _catImageRepository;

        public ImageCategoryController(ICategoryImageRepository categoryImageRepository) {
            _catImageRepository = categoryImageRepository;
        
        }
        public async Task<IActionResult> Index()
        {
            var listImageCats = await _catImageRepository.GetImageCategories();
            return View(listImageCats);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ImageCategory imgCat)
        {
            await _catImageRepository.CreateImageCategory(imgCat);
            return RedirectToAction("Index", "ImageCategory");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var existingImgCat = await _catImageRepository.GetImageCategoryById(id);
            if (existingImgCat == null)
                return BadRequest();
            return View(existingImgCat);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,ImageCategory model)
        {
            var existingImgCat = await _catImageRepository.GetImageCategoryById(id);
            if (existingImgCat == null)
                return BadRequest();

           
            existingImgCat.Name = model.Name;
            existingImgCat.Description = model.Description;

            await _catImageRepository.UpdateImageCategory(existingImgCat);

            return RedirectToAction("Index", "ImageCategory");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var existingImgCat = await _catImageRepository.GetImageCategoryById(id);
            if (existingImgCat == null)
                return BadRequest();

            await _catImageRepository.DeleteImageCategory(existingImgCat);
            return RedirectToAction("Index", "ImageCategory");
        }


    }
}

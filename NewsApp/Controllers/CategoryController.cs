using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using NewsApp.Repository;
using NewsApp.ViewModels;
using System.Net.WebSockets;

namespace NewsApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryReppository _categoryRepository;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryReppository category,IMapper mapper)
        {
            _categoryRepository = category;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _categoryRepository.GetCategories();
            var dto = _mapper.Map<List<CategoryDto>>(model);
            return View(dto);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _categoryRepository.CreateCategory(category);
            return RedirectToAction("Index","Category");
            
        }

        public async Task<IActionResult> Edit(int id)
        {
			var existingCategory = await _categoryRepository.GetCategoryById(id);
            if(existingCategory == null) {
                return View(existingCategory);
            }
			return View(existingCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Category category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }
            var existingCategory = await _categoryRepository.GetCategoryById(id);

            if (existingCategory == null) {
                return View(category);

            }
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            await _categoryRepository.UpdateCategory(existingCategory);

            return RedirectToAction("Index", "Category");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var existingCategory = await _categoryRepository.GetCategoryById(id);

            if (existingCategory == null)
            {
                return View(existingCategory);
            }

            await _categoryRepository.DeleteCategory(existingCategory);
            return RedirectToAction("Index", "Category");
        }
    }
}

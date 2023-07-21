
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using NewsApp.Models;
using NewsApp.ViewModels;
using NewsApp.ViewModels.Seedwork;
using System.Security.Claims;
using NewsApp.Repository;
using NewsApp.Data;

namespace NewsApp.Controllers
{
    public class PostController : Controller
	{
		private readonly IPostRepository _postRepository;
		private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

		public PostController(IPostRepository postRepository,
            ICategoryRepository categoryReppository,
            ICommentRepository commentRepository,
            IRatingRepository ratingRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper) 
        {
			_postRepository = postRepository;
			_categoryRepository = categoryReppository;
            _commentRepository = commentRepository;
            _ratingRepository = ratingRepository;
            _userManager = userManager;
            _mapper = mapper;
		}

		public async Task<IActionResult> Index(Filter filter,int pg=1)
		{
            filter.PageNumber = pg;

            var listCategory = await _categoryRepository.GetCategories();

            filter.SelectListCats = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            ViewBag.Filter = filter;
            if(filter.CategoryIds != null)
            {
                ViewBag.CategoryId = filter.CategoryIds.ToList().FirstOrDefault();
            }
            PagedList<Post> result = await _postRepository.GetPosts(filter);

            return View(result);
        }
       

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create()
		{
            var listCategory = await _categoryRepository.GetCategories();
            PostDto model = new PostDto();

            model.SelectListCats = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }


		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Create(PostDto model)
        { 
			List<PostCategory> postCategories = new List<PostCategory>();

            if (model.CategoryIds.Length > 0)
            {
                foreach (var categoryId in model.CategoryIds)
                {
                    postCategories.Add(new PostCategory
                    {
                        CategoryId = categoryId,
                        PostId = model.Id
                    });
                }
                await _postRepository.CreatePost(new Post
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                    PostCategories = postCategories
                });
            }
            
            return RedirectToAction("Index","Post");	
		}


        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var listCategory = await _categoryRepository.GetCategoriesByPostId(id);

            List<int> categoryIds = new List<int>();

            PostDto model = new PostDto();
            var existingPost = await _postRepository.GetPostById(id);

            if (existingPost == null)
            {
                return RedirectToAction("Index", "Post");
            }

            model.Id = existingPost.Id;
            model.Title = existingPost.Title;
            model.Description = existingPost.Description;
            model.Content = existingPost.Content;
            model.CreatedAt = existingPost.CreatedAt;
            model.SelectListCats = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            ViewBag.PostId = id;
            ViewBag.ListComments = await _commentRepository.GetComments(id);

            ViewBag.ListRatings = await _ratingRepository.GetRatingsByPostId(id);

            return View(model);
        }


        [Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id)
		{
            var listCategory = await _categoryRepository.GetCategories();

            List<int> categoryIds = new List<int>();

            PostDto model = new PostDto();
            var p = await _postRepository.GetPostById(id);

            if(p == null)
            {
                return RedirectToAction("Index", "Post");
            }

            model.SelectListCats = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Id = p.Id;
            model.Title = p.Title;
            model.Description = p.Description;
            model.Content = p.Content;
            model.CreatedAt = p.CreatedAt;
            model.CategoryIds = categoryIds.ToArray();

            return View(model);

        }
		[HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id,PostDto model)
		{
            List<PostCategory> postCategories = new List<PostCategory>();

            var post = await _postRepository.GetPostById(id);
            if (post == null)
                return View(model);

            post.Title = model.Title;
            post.Description = model.Description;
            post.Content = model.Content;
            if (!model.CategoryIds.IsNullOrEmpty())
            {
                postCategories = new List<PostCategory>();

                foreach (var categoryId in model.CategoryIds)
                {
                    postCategories.Add(new PostCategory
                    {
                        CategoryId = categoryId,
                        PostId = model.Id
                    });
                }
                post.PostCategories = postCategories;
            }
            //save
            await _postRepository.UpdatePost(post);

			return RedirectToAction("Index","Post");	
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPost = await _postRepository.GetPostById(id);
            if(existingPost == null)
            {
                return BadRequest();
            }
            await _postRepository.DeletePost(existingPost);
            return RedirectToAction("Index", "Post");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(int postid,string commentmsg)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            ViewBag.UserId = userId;

            var comment = new Comment();
            comment.PostId = postid;
            comment.CommentMsg = commentmsg;
            comment.UserId = userId;
            comment.CreatedTime = DateTime.Now;
            await _commentRepository.CreateComment(comment);

            return Json(comment);

        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RemoveComment(int commentid)
        {
            var existingComment = await _commentRepository.GetCommentById(commentid);
            if (existingComment == null) {
                return BadRequest();
            }
            await _commentRepository.DeleteComment(existingComment);
            return Json(new {success = true});
        }

        [Authorize]
        public async Task<IActionResult> AddOrUpdateRating(int postid,int rate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return BadRequest();
            }
            ViewBag.UserId = userId;


            if(!await _ratingRepository.HasRating(postid, userId))
            {
                var rating = new Rating();
                rating.Rate = rate;
                rating.PostId = postid;
                rating.UserId = userId;
                rating.CreatedAt = DateTime.Now;
                await _ratingRepository.CreateRating(rating);
                return Json(rating);

                
            }
            else
            {
                var existingRating = await _ratingRepository.GetRatingByUserId(userId);
                if (existingRating == null)
                {
                    return BadRequest();
                }
                ViewBag.RateId = existingRating.Id;

                existingRating.Rate = rate;
                await _ratingRepository.Update(existingRating);
                return Json(existingRating);
            }

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRating(int ratingid)
        {
            var existingRating = await _ratingRepository.GetRatingById(ratingid);
            if (existingRating == null)
            {
                return BadRequest();
            }
            await _ratingRepository.Delete(existingRating);
            return Json(new { success = true });
        }


    }
}

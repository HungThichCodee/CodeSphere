using CodeSphere.ApplicationAttributes.ActionAttributes;
using CodeSphere.ApplicationAttributes.Blog.Post;
using CodeSphere.Areas.Administration.Models.Enums;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.Repositories.BlogRepositories;
using CodeSphere.Repositories.PostRepositories;
using CodeSphere.ViewModels.Blog.InputModels;
using CodeSphere.ViewModels.Blog.ViewModels;
using CodeSphere.ViewModels.PostViewModels.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CodeSphere.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository blogRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public BlogController(
            IBlogRepository blogRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.blogRepository = blogRepository;
            this.userManager = userManager;
        }

        /// <summary>
        /// This function will return a list of all Blog Posts.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="search">Current search text which will filter all Blog Posts.</param>
        /// <returns>Returns a view with a collection with all BLog Posts.</returns>
        // DONE!
        [HttpGet]
        [Route("Blog/{page?}/{search?}")]
        public async Task<IActionResult> Index(int? page, string search)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var pageNumber = page ?? 1;

            if (!string.IsNullOrEmpty(search))
            {
                pageNumber = 1;
            }

            var posts = await this.blogRepository.ExtraxtAllPosts(currentUser, search);
            var model = new BlogViewModel
            {
                Posts = posts.ToPagedList(pageNumber, GlobalConstants.BlogPostsOnPage),
                Search = search,
            };

            return this.View(model);
        }

        /// <summary>
        ///  This function will return a View with needed information for a Blog Post creation.
        /// </summary>
        /// <returns>Returns a view with data which is needed to create a Blog Post.</returns>
        // DONE!
        [HttpGet]
        [Authorize]
        [Route("/Blog/CreatePost")]
        [UserBlocked("Index", "Profile")]
        [PostCrudOperations("Index", "Blog", null, ErrorMessages.NoPermissionsToCreateBlogPost)]
        public async Task<IActionResult> CreatePost()
        {
            var model = new CreatePostIndexModel
            {
                Categories = await this.blogRepository.ExtractAllCategoryNames(),
                Tags = await this.blogRepository.ExtractAllTagNames(),
                PostInputModel = new CreatePostInputModel(),
            };

            // Kiểm tra xem có dữ liệu từ AI không
            var isFromAI = this.HttpContext.Session.GetString("IsFromAI");
            if (isFromAI == "true")
            {
                // Pre-fill dữ liệu từ AI
                model.PostInputModel.Title = this.HttpContext.Session.GetString("AiPostTitle");
                model.PostInputModel.Content = this.HttpContext.Session.GetString("AiPostContent");
                model.PostInputModel.CategoryName = this.HttpContext.Session.GetString("AiPostCategory");

                // Đọc TagsNames từ JSON
                var tagsJson = this.HttpContext.Session.GetString("AiPostTags");
                if (!string.IsNullOrEmpty(tagsJson))
                {
                    try
                    {
                        model.PostInputModel.TagsNames = System.Text.Json.JsonSerializer.Deserialize<ICollection<string>>(tagsJson) ?? new HashSet<string>();
                    }
                    catch
                    {
                        model.PostInputModel.TagsNames = new HashSet<string>();
                    }
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// This function will create a new Blog Post.
        /// </summary>
        /// <param name="model">Data Input Model for Blog Post Creation Data.</param>
        /// <returns>Redirect to Page based on IF-ELSE statement over the Input Model.</returns>
        // DONE!
        [HttpPost]
        [Authorize]
        [UserBlocked("Index", "Profile")]
        [PostCrudOperations("Index", "Blog", null, ErrorMessages.NoPermissionsToCreateBlogPost)]
        public async Task<IActionResult> CreatePost(CreatePostIndexModel model)
        {
            if (this.ModelState.IsValid)
            {
                var currentUser = await this.userManager.GetUserAsync(this.User);
                var tuple = await this.blogRepository.CreatePost(model, currentUser);
                this.TempData[tuple.Item1] = tuple.Item2;

                // Xóa session data từ AI sau khi lưu thành công
                if (this.HttpContext.Session.GetString("IsFromAI") == "true")
                {
                    this.HttpContext.Session.Remove("IsFromAI");
                    this.HttpContext.Session.Remove("AiPostTitle");
                    this.HttpContext.Session.Remove("AiPostContent");
                    this.HttpContext.Session.Remove("AiPostCategory");
                    this.HttpContext.Session.Remove("AiPostTags");
                    this.HttpContext.Session.Remove("AiGeneratedContent");
                    this.HttpContext.Session.Remove("AiGeneratedTitle");
                    this.HttpContext.Session.Remove("AiGeneratedTopic");
                }

                return this.RedirectToAction("Index", "Blog");
            }
            else
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.View(model);
            }
        }

        /// <summary>
        /// This function will delete a Blog Post by its ID.
        /// </summary>
        /// <param name="postId">The target Blog Post ID.</param>
        /// <returns>Redirect to Page based on some IF-ELSE statements over the Input Model.</returns>
        [HttpPost]
        [Authorize]
        [Route("/Blog/DeletePost/{postId}")]
        [UserBlocked("Index", "Profile")]
        [PostCrudOperations("Index", "Blog", null, ErrorMessages.NoPermissionsToDeleteBlogPost)]
        public async Task<IActionResult> DeletePost(string postId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var tuple = await this.blogRepository.DeletePost(postId, currentUser);
            this.TempData[tuple.Item1] = tuple.Item2;
            return this.RedirectToAction("Index", "Blog");
        }

        /// <summary>
        /// This function will extract a target Blog Post information.
        /// </summary>
        /// <param name="id">ID of the target Blog Post for editing.</param>
        /// <returns>Returns a View with a data for the target Blog Post.</returns>
        [HttpGet]
        [Route("/Blog/EditPost/{id}")]
        [Authorize]
        [UserBlocked("Index", "Profile")]
        [PostCrudOperations("Index", "Blog", null, ErrorMessages.NoPermissionToEditBlogPost)]
        public async Task<IActionResult> EditPost(string id)
        {
            if (!await this.blogRepository.IsPostExist(id))
            {
                return this.NotFound();
            }

            EditPostInputModel model = await this.blogRepository.ExtractPost(id);
            model.Categories = await this.blogRepository.ExtractAllCategoryNames();
            model.Tags = await this.blogRepository.ExtractAllTagNames();

            return this.View(model);
        }

        /// <summary>
        /// This function will edit an existing Blog Post.
        /// </summary>
        /// <param name="model">Data Input Model for Blog Post Editing Data.</param>
        /// <returns>Redirect to Page based on IF-ELSE statement over the Input Model.</returns>
        [HttpPost]
        [Authorize]
        [UserBlocked("Index", "Profile")]
        [PostCrudOperations("Index", "Blog", null, ErrorMessages.NoPermissionToEditBlogPost)]
        public async Task<IActionResult> EditPost(EditPostInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var currentUser = await this.userManager.GetUserAsync(this.User);
                var tuple = await this.blogRepository.EditPost(model, currentUser);
                this.TempData[tuple.Item1] = tuple.Item2;
                return this.RedirectToAction("Index", "Post", new { postId = model.Id });
            }

            this.TempData["Error"] = ErrorMessages.InvalidInputModel;
            return this.View(model);
        }
    }
}

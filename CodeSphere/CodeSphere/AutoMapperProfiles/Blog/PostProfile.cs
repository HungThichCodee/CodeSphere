using System.Security.Claims;
using AutoMapper;
using CodeSphere.Models.Blog;
using CodeSphere.ViewModels.AllCategories.ViewModels;
using CodeSphere.ViewModels.Blog.ViewModels;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.Home;
using CodeSphere.ViewModels.PostViewModels.InputModels;
using CodeSphere.ViewModels.PostViewModels.ViewModels;
using CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage;
using CodeSphere.ViewModels.PostViewModels.ViewModels.RecentPost;
using CodeSphere.ViewModels.PostViewModels.ViewModels.TopPost;

namespace CodeSphere.AutoMapperProfiles.Blog
{
    public class PostProfile : Profile
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public PostProfile(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            var userId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.CreateMap<Post, BlogPostCardViewModel>()
                .ForMember(
                    dm => dm.CommentsCount,
                    mo => mo.MapFrom(x => x.Comments.Count))
                .ForMember(
                    dm => dm.IsLiked,
                    mo => mo.MapFrom(x => userId != null && x.PostLikes.Any(y => y.UserId == userId && y.IsLiked)))
                .ForMember(
                    dm => dm.IsAuthor,
                    mo => mo.MapFrom(x => userId != null && userId == x.ApplicationUserId))
                .ForMember(
                    dm => dm.IsFavourite,
                    mo => mo.MapFrom(x => userId != null && x.FavouritePosts.Any(z => z.ApplicationUserId == userId && z.IsFavourite)))
                .ForMember(
                    dm => dm.Likers,
                    mo => mo.MapFrom(x => x.PostLikes
                        .Where(pl => pl.IsLiked && pl.ApplicationUser != null)
                        .Select(pl => pl.ApplicationUser)
                        .ToList()))
                .ForMember(
                    dm => dm.Tags,
                    mo => mo.MapFrom(x => x.PostsTags.Select(y => y.Tag)));

            this.CreateMap<Post, PostViewModel>()
                .ForMember(
                    dm => dm.IsLiked,
                    mo => mo.MapFrom(x => userId != null && x.PostLikes.Any(y => y.UserId == userId && y.IsLiked)))
                .ForMember(
                    dm => dm.IsAuthor,
                    mo => mo.MapFrom(x => userId != null && userId == x.ApplicationUserId))
                .ForMember(
                    dm => dm.IsFavourite,
                    mo => mo.MapFrom(x => userId != null && x.FavouritePosts.Any(z => z.ApplicationUserId == userId && z.IsFavourite)))
                .ForMember(
                    dm => dm.Likers,
                    mo => mo.MapFrom(x => x.PostLikes
                        .Where(pl => pl.IsLiked && pl.ApplicationUser != null)
                        .Select(pl => pl.ApplicationUser)))
                .ForMember(
                    dm => dm.Tags,
                    mo => mo.MapFrom(x => x.PostsTags.Select(x => x.Tag).ToList()));

            this.CreateMap<Post, AllCategoriesPostViewModel>();
            this.CreateMap<Post, TopPostViewModel>();
            this.CreateMap<Post, RecentPostViewModel>();

            this.CreateMap<Post, EditPostInputModel>()
                .ForMember(
                    dm => dm.CategoryName,
                    mo => mo.MapFrom(x => x.Category.Name));

            this.CreateMap<Post, HomeLatestPostViewModel>()
                .ForMember(
                    dm => dm.AuthorUsername,
                    mo => mo.MapFrom(x => x.ApplicationUser.UserName))
                .ForMember(
                    dm => dm.CategoryName,
                    mo => mo.MapFrom(x => x.Category.Name));
        }
    }
}

using AutoMapper;
using CodeSphere.Models.Blog;
using CodeSphere.Models.Enums;
using CodeSphere.ViewModels.AllCategories.ViewModels;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.CategoryViewModels.ViewModels.CategoryPage;
using CodeSphere.ViewModels.CategoryViewModels.ViewModels.TopCategory;
using CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;

namespace CodeSphere.AutoMapperProfiles.Blog
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            this.CreateMap<Category, BlogPostCardCategoryViewModel>();
            this.CreateMap<Category, PostCategoryViewModel>();

            this.CreateMap<Category, AllCategoriesCategoryViewModel>()
                .ForMember(
                    dm => dm.ApprovedPostsCount,
                    mo => mo.MapFrom(x => x.Posts.Count(x => x.PostStatus == PostStatus.Approved)))
                .ForMember(
                    dm => dm.BannedPostsCount,
                    mo => mo.MapFrom(x => x.Posts.Count(x => x.PostStatus == PostStatus.Banned)))
                .ForMember(
                    dm => dm.PendingPostsCount,
                    mo => mo.MapFrom(x => x.Posts.Count(x => x.PostStatus == PostStatus.Pending)));

            this.CreateMap<Category, TopCategoryViewModel>()
                .ForMember(
                    dm => dm.PostsCount,
                    mo => mo.MapFrom(x => x.Posts.Count(x => x.PostStatus == PostStatus.Approved)));

            this.CreateMap<Category, CategoryPageCategoryViewModel>();
            this.CreateMap<Category, BlogComponentCategoryViewModel>();
        }
    }
}

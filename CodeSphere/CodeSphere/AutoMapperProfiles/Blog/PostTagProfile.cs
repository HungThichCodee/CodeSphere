using AutoMapper;
using CodeSphere.Models.Blog;
using CodeSphere.ViewModels.Blog.ViewModels;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage;
using CodeSphere.ViewModels.TagViewModels.TagPage;
using CodeSphere.ViewModels.TagViewModels.TopTag;

namespace CodeSphere.AutoMapperProfiles.Blog
{
    public class PostTagProfile : Profile
    {
        public PostTagProfile()
        {
            this.CreateMap<Tag, PostTagViewModel>();

            this.CreateMap<Tag, TopTagViewModel>()
                .ForMember(
                    dm => dm.Count,
                    mo => mo.MapFrom(x => x.TagsPosts.Count));

            this.CreateMap<Tag, BlogPostCardTagViewModel>();
            this.CreateMap<Tag, TagPageTagViewModel>();
        }
    }
}

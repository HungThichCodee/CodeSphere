using AutoMapper;
using CodeSphere.Models.Blog;
using CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage;

namespace CodeSphere.AutoMapperProfiles.Blog
{
    public class PostImageProfile : Profile
    {
        public PostImageProfile()
        {
            this.CreateMap<PostImage, PostPostImageViewModel>();
        }
    }
}

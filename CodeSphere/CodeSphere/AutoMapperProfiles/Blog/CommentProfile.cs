using AutoMapper;
using CodeSphere.Models.Blog;
using CodeSphere.ViewModels.Blog.ViewModels;
using CodeSphere.ViewModels.CommentViewModels.InputModels;
using CodeSphere.ViewModels.CommentViewModels.ViewModels;
using CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage;

namespace CodeSphere.AutoMapperProfiles.Blog
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            this.CreateMap<Comment, PostCommentViewModel>();
            this.CreateMap<Comment, RecentCommentViewModel>();
            this.CreateMap<Comment, EditCommentInputModel>();
        }
    }
}

using AutoMapper;
using CodeSphere.Models.Blog;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;

namespace CodeSphere.AutoMapperProfiles.ViewComponents
{
    public class PendingPostProfile : Profile
    {
        public PendingPostProfile()
        {
            this.CreateMap<PendingPost, PendingPostViewModel>()
                .ForMember(
                    dm => dm.Id,
                    mo => mo.MapFrom(x => x.Post.Id))
                .ForMember(
                    dm => dm.Title,
                    mo => mo.MapFrom(x => x.Post.Title))
                .ForMember(
                    dm => dm.ShortContent,
                    mo => mo.MapFrom(x => x.Post.ShortContent))
                .ForMember(
                    dm => dm.CreatedOn,
                    mo => mo.MapFrom(x => x.Post.CreatedOn))
                .ForMember(
                    dm => dm.Category,
                    mo => mo.MapFrom(x => x.Post.Category));
        }
    }
}

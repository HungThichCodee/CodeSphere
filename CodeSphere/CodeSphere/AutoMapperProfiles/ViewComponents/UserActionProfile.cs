using AutoMapper;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;

namespace CodeSphere.AutoMapperProfiles.ViewComponents
{
    public class UserActionProfile : Profile
    {
        public UserActionProfile()
        {
            this.CreateMap<UserAction, ActivitiesViewModel>();
        }
    }
}

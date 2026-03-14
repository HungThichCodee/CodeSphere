using AutoMapper;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile.UserProfile;

namespace CodeSphere.AutoMapperProfiles.User
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            this.CreateMap<State, ProfileStateViewModel>();
        }
    }
}

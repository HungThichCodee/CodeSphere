using AutoMapper;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile.UserProfile;

namespace CodeSphere.AutoMapperProfiles.User
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            this.CreateMap<ApplicationRole, ProfileRoleViewModel>();
        }
    }
}

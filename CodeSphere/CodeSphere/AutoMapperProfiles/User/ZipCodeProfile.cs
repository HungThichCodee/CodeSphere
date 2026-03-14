using AutoMapper;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile.UserProfile;

namespace CodeSphere.AutoMapperProfiles.User
{
    public class ZipCodeProfile : Profile
    {
        public ZipCodeProfile()
        {
            this.CreateMap<ZipCode, ProfileZipCodeViewModel>();
        }
    }
}

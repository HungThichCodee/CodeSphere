using AutoMapper;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile.UserProfile;

namespace CodeSphere.AutoMapperProfiles.User
{
    public class CountryCodeProfile : Profile
    {
        public CountryCodeProfile()
        {
            this.CreateMap<CountryCode, ProfileCountryCodeViewModel>();
        }
    }
}

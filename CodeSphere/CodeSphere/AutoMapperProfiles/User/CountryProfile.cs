using AutoMapper;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile.UserProfile;

namespace CodeSphere.AutoMapperProfiles.User
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            this.CreateMap<Country, ProfileCountryViewModel>();
        }
    }
}

using AutoMapper;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile.UserProfile;

namespace CodeSphere.AutoMapperProfiles.User
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            this.CreateMap<City, ProfileCityViewModel>();
        }
    }
}

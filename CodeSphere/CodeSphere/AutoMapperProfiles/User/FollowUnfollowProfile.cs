using System.Security.Claims;
using AutoMapper;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;

namespace CodeSphere.AutoMapperProfiles.User
{
    public class FollowUnfollowProfile : Profile
    {
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FollowUnfollowProfile(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;

            var userId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.CreateMap<ApplicationUser, FollowingViewModel>()
                .ForMember(
                    dm => dm.HasFollow,
                    mo => mo.MapFrom(x => this.db.FollowUnfollows
                        .Any(y => y.FollowerId == userId &&
                            y.ApplicationUserId == x.Id &&
                            y.IsFollowed == true)));

            this.CreateMap<ApplicationUser, FollowersViewModel>()
                .ForMember(
                    dm => dm.HasFollow,
                    mo => mo.MapFrom(x => this.db.FollowUnfollows
                        .Any(y => y.FollowerId == userId &&
                            y.ApplicationUserId == x.Id &&
                            y.IsFollowed == true)));
        }
    }
}

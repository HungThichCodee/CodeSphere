using System.Security.Claims;
using AutoMapper;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Blog.ViewModels.BlogPostCard;
using CodeSphere.ViewModels.CommentViewModels.ViewModels;
using CodeSphere.ViewModels.Home;
using CodeSphere.ViewModels.PostViewModels.ViewModels.PostPage;
using CodeSphere.ViewModels.PostViewModels.ViewModels.RecentPost;
using CodeSphere.ViewModels.PostViewModels.ViewModels.TopPost;
using CodeSphere.ViewModels.Profile.UserProfile;
using CodeSphere.ViewModels.Profile.UserViewComponents.ActivitiesComponent;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;
using CodeSphere.ViewModels.Users.ViewModels;

namespace CodeSphere.AutoMapperProfiles.User
{
    public class UserProfile : Profile
    {
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserProfile(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;

            var userId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.CreateMap<ApplicationUser, BlogPostCardApplicationUserViewModel>();
            this.CreateMap<ApplicationUser, BlogPostCardLikerViewModel>();
            this.CreateMap<ApplicationUser, PostApplicationUserViewModel>();
            this.CreateMap<ApplicationUser, PostLikerViewModel>();
            this.CreateMap<ApplicationUser, TopPostApplicationUserViewMdoel>();
            this.CreateMap<ApplicationUser, RecentPostApplicationUserViewModel>();
            this.CreateMap<ApplicationUser, RecentCommentApplicationUserViewModel>();
            this.CreateMap<ApplicationUser, HomeAdministratorUserViewModel>();

            this.CreateMap<ApplicationUser, ProfileApplicationUserViewModel>()
                .ForMember(
                    dm => dm.IsFollowed,
                    mo => mo.MapFrom(x => this.db.FollowUnfollows
                        .Any(y => y.FollowerId == userId && y.ApplicationUserId == x.Id && y.IsFollowed == true)))
                .ForMember(
                    dm => dm.ActionsCount,
                    mo => mo.MapFrom(x => x.UserActions.Count))
                .ForMember(
                    dm => dm.CreatedPosts,
                    mo => mo.MapFrom(x => x.Posts.Count))
                .ForMember(
                    dm => dm.LikedPosts,
                    mo => mo.MapFrom(x => x.PostLikes.Count))
                .ForMember(
                    dm => dm.CommentsCount,
                    mo => mo.MapFrom(x => x.Comments.Count))
                .ForMember(
                    dm => dm.Roles,
                    mo => mo.MapFrom(x => x.UserRoles.Select(y => y.Role)));

            this.CreateMap<ApplicationUser, ActivitiesApplicationUserViewModel>();
            this.CreateMap<ApplicationUser, BlogComponentApplicationUserViewModel>();

            this.CreateMap<ApplicationUser, AllUsersUserCardViewModel>()
                .ForMember(
                    dm => dm.Activities,
                    mo => mo.MapFrom(x => x.UserActions.Count))
                .ForMember(
                    dm => dm.FollowersCount,
                    mo => mo.MapFrom(x => this.db.FollowUnfollows.Count(y => y.ApplicationUserId == x.Id && y.IsFollowed == true)))
                .ForMember(
                    dm => dm.FollowingsCount,
                    mo => mo.MapFrom(x => this.db.FollowUnfollows.Count(y => y.FollowerId == x.Id && y.IsFollowed == true)))
                .ForMember(
                    dm => dm.HasFollowed,
                    mo => mo.MapFrom(x => this.db.FollowUnfollows
                        .Any(y => y.FollowerId == userId && y.ApplicationUserId == x.Id && y.IsFollowed == true)));
        }
    }
}

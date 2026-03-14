using AutoMapper;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Profile;
using CodeSphere.ViewModels.Profile.UserViewComponents.BlogComponent;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.Profile
{
    public class ProfileFavouritePostsRepository : IProfileFavoritesRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProfileFavouritePostsRepository(
            ApplicationDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public List<FavouritePostViewModel> ExtractFavorites(ApplicationUser user, ApplicationUser currentUser)
        {
            var favorites = this.db.FavouritePosts
                .Where(x => x.ApplicationUserId == user.Id && x.IsFavourite == true)
                .Include(x => x.Post)
                .ThenInclude(x => x.Category)
                .Include(x => x.ApplicationUser)
                .OrderByDescending(x => x.Post.CreatedOn)
                .AsSplitQuery()
                .ToList();

            var model = this.mapper.Map<List<FavouritePostViewModel>>(favorites);
            return model;
        }
    }
}

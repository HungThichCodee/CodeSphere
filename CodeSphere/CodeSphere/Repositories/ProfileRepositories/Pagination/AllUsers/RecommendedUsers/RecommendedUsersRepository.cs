using System.Linq.Expressions;
using AutoMapper;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.RecommendedUsers
{
    public class RecommendedUsersRepository : IRecommendedUsersRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public RecommendedUsersRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<AllUsersUserCardViewModel>> ExtractAllUsers(string username, string search)
        {
            Expression<Func<RecommendedFriend, bool>> usersFilter;

            if (search == null)
            {
                usersFilter = x => x.ApplicationUser.UserName == username;
            }
            else
            {
                usersFilter = x => (EF.Functions.FreeText(x.RecommendedApplicationUser.UserName, search) ||
                    EF.Functions.FreeText(x.RecommendedApplicationUser.FirstName, search) ||
                     EF.Functions.FreeText(x.RecommendedApplicationUser.LastName, search)) &&
                     x.ApplicationUser.UserName == username;
            }

            var users = await this.db.RecommendedFriends
                .Where(usersFilter)
                .Include(x => x.RecommendedApplicationUser)
                .Select(x => x.RecommendedApplicationUser)
                .AsSplitQuery()
                .ToListAsync();

            var model = this.mapper.Map<List<AllUsersUserCardViewModel>>(users);
            return model;
        }
    }
}

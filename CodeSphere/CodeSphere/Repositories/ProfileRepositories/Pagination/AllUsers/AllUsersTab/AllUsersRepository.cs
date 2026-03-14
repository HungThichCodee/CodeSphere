using System.Linq.Expressions;
using AutoMapper;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.AllUsersTab;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers
{
    public class AllUsersRepository : IAllUsersRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public AllUsersRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<AllUsersUserCardViewModel>> ExtractAllUsers(string username, string search)
        {
            Expression<Func<ApplicationUser, bool>> usersFilter;

            if (string.IsNullOrWhiteSpace(search))
            {
                usersFilter = x => !x.IsBlocked;
            }
            else
            {
                string pattern = $"%{search}%";
                usersFilter = x => (
                    EF.Functions.Like(x.UserName, pattern) ||
                    EF.Functions.Like(x.FirstName, pattern) ||
                    EF.Functions.Like(x.LastName, pattern)
                ) && !x.IsBlocked;
            }

            List<ApplicationUser> users = await this.db.Users
                .Where(usersFilter)
                .Include(x => x.UserActions)
                .AsSplitQuery()
                .ToListAsync();

            List<AllUsersUserCardViewModel> model = this.mapper.Map<List<AllUsersUserCardViewModel>>(users);
            return model;
        }
    }
}

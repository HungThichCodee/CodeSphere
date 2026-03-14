using System.Linq.Expressions;
using AutoMapper;
using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.User;
using CodeSphere.ViewModels.Users.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.ProfileRepositories.Pagination.AllUsers.AllAdministrators
{
    public class AllAdministratorsRepository : IAllAdministratorsRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public AllAdministratorsRepository(
            ApplicationDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<AllUsersUserCardViewModel>> ExtractAllUsers(string username, string search)
        {
            Expression<Func<ApplicationUser, bool>> usersFilter;

            if (search == null)
            {
                usersFilter = x => x.UserRoles.Any(x => x.Role.Name == GlobalConstants.AdministratorRole);
            }
            else
            {
                usersFilter = x => (EF.Functions.FreeText(x.UserName, search) ||
                     EF.Functions.FreeText(x.FirstName, search) ||
                     EF.Functions.FreeText(x.LastName, search)) &&
                     x.UserRoles.Any(y => y.Role.Name == GlobalConstants.AdministratorRole);
            }

            var users = await this.db.Users
                .Where(usersFilter)
                .Include(x => x.UserActions)
                .AsSplitQuery()
                .ToListAsync();

            var model = this.mapper.Map<List<AllUsersUserCardViewModel>>(users);
            return model;
        }
    }
}

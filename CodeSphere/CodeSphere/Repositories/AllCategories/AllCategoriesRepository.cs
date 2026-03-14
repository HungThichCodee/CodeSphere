using AutoMapper;
using CodeSphere.Data;
using CodeSphere.Models.Enums;
using CodeSphere.ViewModels.AllCategories.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Repositories.AllCategories
{
    public class AllCategoriesRepository : IAllCategoriesRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public AllCategoriesRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public ICollection<AllCategoriesCategoryViewModel> GetAllBlogCategories()
        {
            var categories = this.db.Categories
                .Include(x => x.Posts)
                .ThenInclude(x => x.ApplicationUser)
                .OrderBy(x => x.Name)
                .ToList();

            var model = this.mapper.Map<List<AllCategoriesCategoryViewModel>>(categories);
            return model;
        }
    }
}

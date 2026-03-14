using CodeSphere.Constraints;
using CodeSphere.Data;
using CodeSphere.Models.Blog;

namespace CodeSphere.Areas.Editor.Repositories.CategoryRepositories
{
    public class AddCategoryRepository : IAddCategoryRepository
    {
        private readonly ApplicationDbContext db;

        public AddCategoryRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Tuple<string, string>> CreateCategory(string name, string description)
        {
            if (this.db.Categories.Any(x => x.Name.ToLower() == name.ToLower()))
            {
                return Tuple.Create("Error", string.Format(ErrorMessages.CategoryAlreadyExist, name));
            }

            var category = new Category
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Description = description,
            };

            this.db.Categories.Add(category);
            await this.db.SaveChangesAsync();
            return Tuple.Create("Success", string.Format(SuccessMessages.SuccessfullyAddedCategory, name));
        }
    }
}

using CodeSphere.Areas.Editor.ViewModels;
using CodeSphere.Data;
using CodeSphere.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSphere.Areas.Editor.Repositories.CategoryRepositories
{
    public class EditCategoryRepository : IEditCategoryRepository
    {
        private readonly ApplicationDbContext db;

        public EditCategoryRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> EditCategory(EditCategoryInputModel model)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (category != null)
            {
                category.Name = model.Name;
                category.Description = model.SanitizedDescription;
                category.UpdatedOn = DateTime.UtcNow;
                this.db.Categories.Update(category);
                await this.db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<EditCategoryInputModel> ExtractCategoryById(string id)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(x => x.Id == id);

            return new EditCategoryInputModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}

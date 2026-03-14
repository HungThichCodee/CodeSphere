using CodeSphere.Areas.Editor.ViewModels;
using CodeSphere.Repositories;

namespace CodeSphere.Areas.Editor.Repositories.CategoryRepositories
{
    public interface IEditCategoryRepository
    {
        Task<EditCategoryInputModel> ExtractCategoryById(string id);

        Task<bool> EditCategory(EditCategoryInputModel model);
    }
}

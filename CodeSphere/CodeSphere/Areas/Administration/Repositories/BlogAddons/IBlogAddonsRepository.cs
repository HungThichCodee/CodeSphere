using CodeSphere.Areas.Administration.ViewModels.BlogAddonsViewModels.ViewModels;
using CodeSphere.Areas.Editor.ViewModels;

namespace CodeSphere.Areas.Administration.Repositories.BlogAddons
{
    public interface IBlogAddonsRepository
    {
        Task<Tuple<string, string>> CreateCategoryAdminArea(string name, string description);

        Task<Tuple<string, string>> CreateTag(string name);

        Task<Tuple<string, string>> RemoveTag(string name);

        ICollection<string> GetAllTags();

        ICollection<EditCategoryViewModel> GetAllCategories();

        Task<GetCategoryDataViewModel> GetCategoryById(string categoryId);

        Task EditExistingCategory(EditCategoryInputModel model);
    }
}

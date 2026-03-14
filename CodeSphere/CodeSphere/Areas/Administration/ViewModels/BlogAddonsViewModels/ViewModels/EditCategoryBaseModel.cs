using CodeSphere.Areas.Editor.ViewModels;

namespace CodeSphere.Areas.Administration.ViewModels.BlogAddonsViewModels.ViewModels
{
    public class EditCategoryBaseModel
    {
        public ICollection<EditCategoryViewModel> EditCategoryViewModels { get; set; } =
            new HashSet<EditCategoryViewModel>();

        public EditCategoryInputModel EditCategoryInputModel { get; set; } = new EditCategoryInputModel();
    }
}

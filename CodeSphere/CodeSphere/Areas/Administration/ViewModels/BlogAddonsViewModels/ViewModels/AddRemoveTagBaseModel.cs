using CodeSphere.Areas.Administration.ViewModels.BlogAddonsViewModels.InputModels;

namespace CodeSphere.Areas.Administration.ViewModels.BlogAddonsViewModels.ViewModels
{
    public class AddRemoveTagBaseModel
    {
        public ICollection<string>? TagsNames { get; set; }

        public AddRemoveTagInputModel? AddRemoveTagInputModel { get; set; }
    }
}

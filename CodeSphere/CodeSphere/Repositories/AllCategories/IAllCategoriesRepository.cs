using CodeSphere.ViewModels.AllCategories.ViewModels;

namespace CodeSphere.Repositories.AllCategories
{
    public interface IAllCategoriesRepository
    {
        ICollection<AllCategoriesCategoryViewModel> GetAllBlogCategories();
    }
}

namespace CodeSphere.Areas.Editor.Repositories.CategoryRepositories
{
    public interface IAddCategoryRepository
    {
        Task<Tuple<string, string>> CreateCategory(string name, string description);
    }
}

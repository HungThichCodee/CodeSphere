using CodeSphere.Areas.Administration.ViewModels.AddEmojis.InputModels;

namespace CodeSphere.Areas.Administration.Repositories.AddEmojis
{
    public interface IAddEmojisRepository
    {
        Task<string> AddEmojis(AddEmojisInputModel model);
    }
}

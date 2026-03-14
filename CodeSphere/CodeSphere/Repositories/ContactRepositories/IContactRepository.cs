using CodeSphere.ViewModels.Contacts;

namespace CodeSphere.Repositories.ContactRepositories
{
    public interface IContactRepository
    {
        void SendEmail(ContactInputModel model);
    }
}

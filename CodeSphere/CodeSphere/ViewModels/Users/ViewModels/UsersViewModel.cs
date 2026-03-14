using CodeSphere.Models.Enums;

namespace CodeSphere.ViewModels.Users.ViewModels
{
    public class UsersViewModel
    {
        public int Page { get; set; }

        public AllUsersTab ActiveTab { get; set; }

        public string Search { get; set; }
    }
}
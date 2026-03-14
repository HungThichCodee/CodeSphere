using CodeSphere.Data;

namespace CodeSphere.Areas.PrivateChat.Repositories.ChatMessagesDbUsage
{
    public class DeleteMessages : IDeleteMessages
    {
        private readonly ApplicationDbContext db;

        public DeleteMessages(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void DeleteAllChatMessages()
        {
            var target = this.db.ChatMessages.ToList();
            this.db.RemoveRange(target);
            this.db.SaveChanges();
        }
    }
}

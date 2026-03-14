using CodeSphere.Data;
using CodeSphere.Models.Enums;

namespace CodeSphere.Repositories.UserActivitesDbUsage.AllActivities
{
    public class AllActivities : IAllActivities
    {
        private readonly ApplicationDbContext db;

        public AllActivities(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void DeleteAllActivites()
        {
            var target = this.db.UserActions
                .Where(x => x.ActionStatus != UserActionStatus.Pinned)
                .ToList();
            this.db.RemoveRange(target);
            this.db.SaveChanges();
        }
    }
}

using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using CodeSphere.Areas.Administration.Models.HolidayTheme;
using CodeSphere.Areas.PrivateChat.Models;
using CodeSphere.Areas.UserNotifications.Models;
using CodeSphere.Models.Blog;
using CodeSphere.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using State = CodeSphere.Models.User.State;

namespace CodeSphere.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
         IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
         IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FollowUnfollow> FollowUnfollows { get; set; }

        public DbSet<UserAction> UserActions { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostImage> PostImages { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostsTags { get; set; }

        public DbSet<PostLike> PostsLikes { get; set; }

        public DbSet<FavouritePost> FavouritePosts { get; set; }

        public DbSet<PendingPost> PendingPosts { get; set; }

        public DbSet<BlockedPost> BlockedPosts { get; set; }

        public DbSet<ZipCode> ZipCodes { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<CountryCode> CountryCodes { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<UserGroup> UsersGroups { get; set; }

        public DbSet<ChatTheme> ChatThemes { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<ChatImage> ChatImages { get; set; }

        public DbSet<RecommendedFriend> RecommendedFriends { get; set; }

        public DbSet<UserRating> UserRatings { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }

        public DbSet<Emoji> Emojis { get; set; }

        public DbSet<EmojiSkin> EmojiSkins { get; set; }

        public DbSet<StickerType> StickerTypes { get; set; }

        public DbSet<Sticker> Stickers { get; set; }

        public DbSet<FavouriteStickers> FavouriteStickers { get; set; }

        public DbSet<HolidayTheme> HolidayThemes { get; set; }

        public DbSet<HolidayIcon> HolidayIcons { get; set; }

        public DbSet<QuickChatReply> QuickChatReplies { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("ApplicationUsers");

            builder.Entity<ApplicationRole>().ToTable("ApplicationRoles");

            builder.Entity<ApplicationUserRole>().ToTable("ApplicationUsersRoles");

            builder.Entity<Post>(entity =>
            {
                entity.HasOne(x => x.ApplicationUser)
                    .WithMany(x => x.Posts)
                    .HasForeignKey(x => x.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.Comments)
                    .WithOne(x => x.Post)
                    .HasForeignKey(x => x.PostId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Category)
                    .WithMany(x => x.Posts)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.PostImages)
                   .WithOne(x => x.Post)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<City>(entity =>
            {
                entity.HasOne(x => x.State)
                    .WithMany(x => x.Cities)
                    .HasForeignKey(x => x.StateId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Country)
                    .WithMany(x => x.Cities)
                    .HasForeignKey(x => x.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.ZipCodes)
                    .WithOne(x => x.City)
                    .HasForeignKey(x => x.CityId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(x => x.ZipCode)
                    .WithMany(x => x.ApplicationUsers)
                    .HasForeignKey(x => x.ZipCodeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(x => x.CountryCode)
                    .WithMany(x => x.ApplicationUsers)
                    .HasForeignKey(x => x.CountryCodeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(x => x.Country)
                    .WithMany(x => x.ApplicationUsers)
                    .HasForeignKey(x => x.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.State)
                    .WithMany(x => x.ApplicationUsers)
                    .HasForeignKey(x => x.StateId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.City)
                    .WithMany(x => x.ApplicationUsers)
                    .HasForeignKey(x => x.CityId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            builder.Entity<FollowUnfollow>(b =>
            {
                b.HasOne(e => e.ApplicationUser)
                    .WithMany(e => e.Followers)
                    .HasForeignKey(e => e.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<FollowUnfollow>(b =>
            {
                b.HasOne(e => e.Follower)
                    .WithMany(e => e.Following)
                    .HasForeignKey(e => e.FollowerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<RecommendedFriend>(b =>
            {
                b.HasOne(e => e.ApplicationUser)
                    .WithMany(e => e.RecommendedFriends)
                    .HasForeignKey(e => e.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<RecommendedFriend>(b =>
            {
                b.HasOne(e => e.RecommendedApplicationUser)
                    .WithMany(e => e.UserRecommendations)
                    .HasForeignKey(e => e.RecommendedApplicationUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Comment>(entity =>
            {
                entity.HasOne(c => c.ParentComment)
                    .WithMany()
                    .HasForeignKey(c => c.ParentCommentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<PostTag>().HasKey(k => new
            {
                k.TagId,
                k.PostId,
            });

            builder.Entity<PostLike>().HasKey(k => new
            {
                k.PostId,
                k.UserId,
            });

            builder.Entity<FollowUnfollow>().HasKey(k => new
            {
                k.ApplicationUserId,
                k.FollowerId,
            });

            builder.Entity<RecommendedFriend>().HasKey(k => new
            {
                k.ApplicationUserId,
                k.RecommendedApplicationUserId,
            });

            builder.Entity<FavouritePost>().HasKey(k => new
            {
                k.ApplicationUserId,
                k.PostId,
            });

            builder.Entity<PendingPost>().HasKey(k => new
            {
                k.ApplicationUserId,
                k.PostId,
            });

            builder.Entity<BlockedPost>().HasKey(k => new
            {
                k.ApplicationUserId,
                k.PostId,
            });

            builder.Entity<UserGroup>().HasKey(k => new
            {
                k.GroupId,
                k.ApplicationUserId,
            });

            builder.Entity<UserRating>().HasKey(k => new
            {
                k.Username,
                k.RaterUsername,
            });

            builder.Entity<FavouriteStickers>().HasKey(k => new
            {
                k.ApplicationUserId,
                k.StickerTypeId,
            });

            builder.Entity<Group>(entity =>
            {
                entity.HasOne(x => x.ChatTheme)
                    .WithMany(x => x.Groups)
                    .HasForeignKey(x => x.ChatThemeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasMany(x => x.ChatImages)
                    .WithOne(x => x.Group)
                    .HasForeignKey(x => x.GroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Emoji>()
                .HasMany(x => x.EmojiSkins)
                .WithOne(x => x.Emoji)
                .HasForeignKey(x => x.EmojiId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChatMessage>()
                .HasMany(x => x.ChatImages)
                .WithOne(x => x.ChatMessage)
                .HasForeignKey(x => x.ChatMessageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Country>()
                .HasOne(x => x.CountryCode)
                .WithMany(x => x.Coutries)
                .HasForeignKey(x => x.CountryCodeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sticker>()
                .HasOne(x => x.StickerType)
                .WithMany(x => x.Stickers)
                .HasForeignKey(x => x.StickerTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder.Entity<HolidayIcon>()
                .HasOne(e => e.HolidayTheme)
                .WithMany(e => e.HolidayIcons)
                .HasForeignKey(e => e.HolidayThemeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder.Entity<QuickChatReply>()
                .HasOne(e => e.ApplicationUser)
                .WithMany(e => e.QuickChatReplies)
                .HasForeignKey(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

        }
    }
}

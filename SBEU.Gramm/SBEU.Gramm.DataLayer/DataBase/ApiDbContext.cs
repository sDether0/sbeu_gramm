using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces;
using SBEU.Gramm.DataLayer.DataBase.Entities.Relate;


namespace SBEU.Gramm.DataLayer.DataBase
{
    public class ApiDbContext : IdentityDbContext<XIdentityUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<NCommentary> Commentaries { get; set; }
        public virtual DbSet<NContentObject> ContentObjects { get; set; }
        public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public virtual DbSet<NLike> Likes { get; set; }
        public virtual DbSet<NPost> Posts { get; set; }
        public virtual DbSet<NStory> Stories { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<XIdentityUserConfirm> UserConfirmations { get; set; }
        public virtual DbSet<NNotification> Notifications { get; set; }
        
        /// <summary>
        /// "The Author property of the NPost and NStory classes are both of type XIdentityUser, and the
        /// Posts and Stories properties of the XIdentityUser class are both of type ICollection<NPost>
        /// and ICollection<NStory> respectively
        /// </summary>
        /// <param name="ModelBuilder">The ModelBuilder instance to use.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<NPost>().HasOne(x => x.Author).WithMany(x => x.Posts);
            builder.Entity<NStory>().HasOne(x => x.Author).WithMany(x => x.Stories);
            builder.Entity<XIdentityUser>().HasMany(x => x.PTaggedIn).WithMany(x => x.TaggedUsers);
            builder.Entity<XIdentityUser>().HasMany(x => x.STaggedIn).WithMany(x => x.TaggedUsers);
            builder.Entity<XIdentityUser>().HasMany(x => x.WatchedStories).WithMany(x => x.WatchedUsers);
            builder.Entity<XIdentityUser>().HasMany(x => x.WatchedPosts).WithMany(x => x.WatchedUsers);
            builder.Entity<XIdentityUser>().HasOne(x => x.Image).WithMany();
            builder.Entity<XIdentityUser>().HasMany(x => x.ContentsPosition).WithOne(x => x.User)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<XIdentityUserContent>().HasOne(x => x.Content).WithMany(x => x.UsersPosition)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<XIdentityUserContent>().HasKey(x => new { x.UserId,x.ContentId });
            builder.Entity<NNotification>().HasOne(x => x.Target).WithMany(x => x.Notifications);
            builder.Entity<NNotification>().HasOne(x => x.Post).WithMany();
            builder.Entity<NNotification>().HasOne(x => x.Story).WithMany();
            builder.Entity<NNotification>().HasOne(x => x.Commentary).WithMany();
            builder.Entity<NNotification>().HasOne(x => x.Author).WithMany();
            builder.Entity<NPost>().HasMany(x => x.Contents).WithMany(x => x.Posts).UsingEntity<NPostContent>();
            builder.Entity<NPostContent>().HasKey(x => new { ContentId = x.ContentsId, PostId = x.PostsId});
            builder.Entity<XIdentityUser>().HasMany(x => x.Contents).WithMany(x => x.Users).UsingEntity<XIdentityUserContent>();
            
            base.OnModelCreating(builder);
        }


        /// <summary>
        /// It takes an id, and returns the entity that has that id
        /// </summary>
        /// <param name="id">the id of the entity</param>
        /// <returns>
        /// The return type is an interface.
        /// </returns>
        public async Task<ILikable> FindEntity(string id)
        {
            if (Posts.Any(x => x.Id == id))
            {
                return await Posts.FirstAsync(x => x.Id == id);
            }
            if (ContentObjects.Any(x => x.Id == id))
            {
                return await ContentObjects.FirstAsync(x => x.Id == id);
            }
            if (Commentaries.Any(x => x.Id == id))
            {
                return await Commentaries.FirstAsync(x => x.Id == id);
            }
            if (Stories.Any(x => x.Id == id))
            {
                return await Stories.FirstAsync(x => x.Id == id);
            }

            throw new Exception("Likable entity not found");
        }
    }
}

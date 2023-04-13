using Microsoft.EntityFrameworkCore;
using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;
using SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces;
using SBEU.Gramm.DataLayer.DataBase.Entities.Relate;
using SBEU.Gramm.Middleware.Repositories.Interfaces;
using SBEU.Gramm.Models.Interfaces;
using SBEU.Gramm.Models.Responses;

namespace SBEU.Gramm.Middleware.Repositories
{
    /* It's a class that fills the entity with the data from the database */
    public static class FillRepository
    {
        /// <summary>
        /// It takes a post object, and fills it with the contents, tags and tagged users from the
        /// database
        /// </summary>
        /// <param name="NPost">The model that is being filled</param>
        /// <param name="ApiDbContext">The context of the database</param>
        public static void Fill(this NPost post, ApiDbContext context)
        {
            post.CreatedAt= DateTime.UtcNow;
            ushort j = 0;
            post.Contents = post.Contents
                .Select(x => context.ContentObjects.Find(x.Id) ?? throw Exceptions.EntityNotFound<NContentObject>()).ToList();
            post.Position = post.Contents
                .Select(x  => new NPostContent(){ ContentsId = x.Id, PostsId = post.Id,Position = j++}??throw Exceptions.EntityNotFound<NContentObject>())
                .ToList();
            post.TaggedUsers = post.TaggedUsers.Select(x => context.Users.Find(x.Id)!).ToList();
            ulong i = 1;       
            post.Tags = post.Tags.Select(x =>
                {
                    var tag = context.Tags.FirstOrDefault(s => s.Tag == x.Tag) ?? new Tags() { Id= (context.Tags.Max(m=>m.Id)+(i++)), Tag = x.Tag };
                    tag.Popularity++;
                    return tag;
                }
            ).ToList();
        }
        public static void Fill(this NStory story, ApiDbContext context)
        {
            story.CreatedAt = DateTime.UtcNow;
            story.SetSound(context.ContentObjects.Find(story.Content.Id) ?? throw Exceptions.EntityNotFound<NContentObject>());
            story.Content = context.ContentObjects.Find(story.Content.Id) ?? throw Exceptions.EntityNotFound<NContentObject>();
            story.TaggedUsers = story.TaggedUsers.Select(x => context.Users.Find(x.Id)!).ToList();
        }

        /// <summary>
        /// > This function is used to fill the comment with the current date and time and the post that
        /// the comment is associated with
        /// </summary>
        /// <param name="NCommentary">The model that is being filled</param>
        /// <param name="ApiDbContext">The context of the database.</param>
        public static void Fill(this NCommentary comment, ApiDbContext context)
        {
            comment.CreatedAt = DateTime.UtcNow;
            comment.Post = context.Posts.Find(comment.Post.Id)??throw Exceptions.EntityNotFound<NPost>();
        }

        public static void Fill(this NContentObject content, ApiDbContext context)
        {
            content.CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// It takes an entity and a context, and then it fills the entity with the appropriate data
        /// from the context
        /// </summary>
        /// <param name="T">BaseEntity</param>
        /// <param name="ApiDbContext">The database context</param>
        public static void Fill<T>(this T entity, ApiDbContext context) where T : BaseEntity
        {
            switch (entity)
            {
                case NPost post:
                    post.Fill(context);
                    break;
                case NStory story:
                    story.Fill(context);
                    break;
                case NCommentary commentary:
                    commentary.Fill(context);
                    break;
                case NContentObject content:
                    content.Fill(context);
                    break;
            }
        }

        /// <summary>
        /// > This function checks if a user has liked a content
        /// </summary>
        /// <param name="IIsLike">The interface that the entity implements.</param>
        /// <param name="contents">The collection of contents that you want to check if the entity is
        /// liked in.</param>
        /// <param name="userId">The userId of the user who is liking the content.</param>
        /// <returns>
        /// A boolean value.
        /// </returns>
        public static bool IsLiked<T>(this IIsLike entity, IEnumerable<T> contents, string userId) where T: ILikable
        {
            return contents.First(x=>x.Id==entity.Id).Likes.Any(x=>x.Author.Id==userId);
        }

        /// <summary>
        /// > This function checks if a user has watched a story
        /// </summary>
        /// <param name="NStory">The model that contains the list of users that have watched the
        /// story</param>
        /// <param name="userId">The user's id</param>
        /// <returns>
        /// A boolean value.
        /// </returns>
        public static bool IsWatched(this NStory nStory, string userId)
        {
            return nStory.WatchedUsers.Any(x => x.Id == userId);
        }

        /// <summary>
        /// This function checks if the user is following the userId
        /// </summary>
        /// <param name="XIdentityUser">The user model</param>
        /// <param name="userId">The userId of the user you want to check if the current user is
        /// following.</param>
        /// <returns>
        /// A boolean value.
        /// </returns>
        public static bool IsFollow(this XIdentityUser user, string userId)
        {
            return user.Followers.Any(x => x.Id == userId);
        }

        /// <summary>
        /// It returns all the likes of a given content object
        /// </summary>
        /// <param name="ILikable">The interface that all of the content types implement.</param>
        /// <param name="ApiDbContext">The database context</param>
        /// <returns>
        /// IEnumerable<NLike>
        /// </returns>
        public static IEnumerable<NLike> GetLikes(this ILikable content, ApiDbContext context)
        {
            switch (content)
            {
                case NPost post:
                    return context.Likes.Where(x=>x.Post.Id==post.Id);
                case NStory story:
                    return context.Likes.Where(x => x.Story.Id == story.Id);
                case NCommentary commentary:
                    return context.Likes.Where(x => x.Commentary.Id == commentary.Id);
                case NContentObject contentObject:
                    return context.Likes.Where(x => x.Content.Id == contentObject.Id);
            }
            return null;
        }
    }
}

using System.Globalization;
using AutoMapper;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Models.Requests.Create;
using SBEU.Gramm.Models.Responses;

namespace SBEU.Gramm.Api.Models
{
    public class InitProfile : Profile
    {
        public InitProfile()
        {
            
            CreateMap<XIdentityUser, SmallUserDto>()
                .ForMember(x => x.Id, s => s.MapFrom(o => o.Id))
                .ForMember(x => x.Image, s => s.MapFrom(o=>o.Image.Link))
                .ForMember(x => x.NickName, s => s.MapFrom(o => o.UserName));
            CreateMap<SmallUserDto, XIdentityUser>()
                .ForMember(x => x.Id, s => s.MapFrom(o => o.Id));
            CreateMap<NLike, LikeDto>()
                .ForMember(x => x.Author, s => s.MapFrom(o => o.Author));
                
            
            CreateMap<NCommentary, CommentaryDto>()
                .ForMember(x=>x.PostId,s=>s.MapFrom(o=>o.Post.Id))
                .ForMember(x => x.LikesCount, s => s.MapFrom(o => o.Likes.Count))
                .ForMember(x => x.Author, s => s.MapFrom(o => o.Author));
            
            CreateMap<NStory, StoryDto>()
                .ForMember(x => x.Author, s => s.MapFrom(o => o.Author))
                .ForMember(x => x.LikesCount, s => s.MapFrom(o => o.Likes.Count))
                .ForMember(x => x.Tagged,
                    s => s.MapFrom(o => o.TaggedUsers))
                .ForMember(x => x.Watched,
                    s => s.MapFrom(o => o.WatchedUsers))
                .ForMember(x=>x.Content,s=>s.MapFrom(o=>o.Content.Link));

            CreateMap<NPost, PostDto>()
                .ForMember(x => x.ViewsCount, s => s.MapFrom(o => o.WatchedUsers.Count))
                .ForMember(x => x.Author, s => s.MapFrom(o => o.Author))
                .ForMember(x => x.LikesCount, s => s.MapFrom(o => o.Likes.Count))
                .ForMember(x => x.Tagged,
                    s => s.MapFrom(o => o.TaggedUsers))
                .ForMember(x => x.Commentaries,
                    s => s.MapFrom(o => o.Commentaries))
                .ForMember(x => x.Content, s => s.MapFrom(o => o.Contents))
                .ForMember(x => x.Tags, s => s.MapFrom(o => o.Tags.Select(x => x.Tag)));

            CreateMap<NPost, SmallPostDto>()
                .ForMember(x=>x.ViewsCount,s=>s.MapFrom(o=>o.WatchedUsers.Count))
                .ForMember(x => x.Author, s => s.MapFrom(o =>o.Author))
                .ForMember(x => x.Content, s => s.MapFrom(o => o.Contents.OrderBy(x=>x.PostPositions.First(s=>s.PostsId==o.Id).Position)))
                .ForMember(x => x.CommentariesCount, s => s.MapFrom(o => o.Commentaries.Count))
                .ForMember(x => x.LikesCount, s => s.MapFrom(o => o.Likes.Count));

            CreateMap<XIdentityUser, UserDto>()
                .ForMember(x=>x.NickName,s=>s.MapFrom(o=>o.UserName))
                .ForMember(x => x.Image, s => s.MapFrom(o => o.Image))
                .ForMember(x => x.Followers,
                    s => s.MapFrom(o => o.Followers))
                .ForMember(x => x.Following,
                    s => s.MapFrom(o => o.Following))
                .ForMember(x => x.Posts, s => s.MapFrom(o => o.Posts));
                

            CreateMap<CommentaryDto, NCommentary>()
                .ForMember(x => x.Author, s => s.Ignore())//MapFrom(o => new XIdentityUser() { Id = o.Author.Id }))
                .ForMember(x => x.Post, s => s.MapFrom(o => o.PostId))
                .ForMember(x=>x.Likes,s=>s.Ignore());

            CreateMap<PostDto, NPost>()
                .ForMember(x => x.TaggedUsers, s => s.Ignore()) //MapFrom(o => o.Tagged.Select(x => new NTaggedUser() { User = new XIdentityUser() { Id = x.Id }, UTagObject = new NPost() { Id = o.Id },Id = Guid.NewGuid().ToString()})))
                .ForMember(x => x.Tags, s => s.MapFrom(o => o.Tags.Select(x => new Tags() { Tag = x })))
                .ForMember(x=>x.Contents,s=>s.MapFrom(o=>o.Content))
                .ForMember(x=>x.Author,s=>s.Ignore())
                .ForMember(x=>x.Likes,s=>s.Ignore())
                .ForMember(x=>x.Commentaries,s=>s.Ignore());
            
            CreateMap<StoryDto, NStory>()
                .ForMember(x => x.TaggedUsers, s => s.MapFrom(o=>o.Tagged))
                .ForMember(x=>x.Author,s=>s.Ignore())
                .ForMember(x => x.Likes, s => s.Ignore())
                .ForMember(x => x.WatchedUsers, s => s.Ignore());
            CreateMap<string, NContentObject>()
                .ForMember(x => x.Id, s => s.MapFrom(o => o));
            CreateMap<string, NPost>()
                .ForMember(x => x.Id, s => s.MapFrom(o => o));

            CreateMap<CreatePostDto, NPost>()
                .ForMember(x => x.TaggedUsers, s => s.MapFrom(o=>o.Tagged)) //MapFrom(o => o.Tagged.Select(x => new NTaggedUser() { User = new XIdentityUser() { Id = x.Id }, UTagObject = new NPost() { Id = o.Id },Id = Guid.NewGuid().ToString()})))
                .ForMember(x => x.Tags, s => s.MapFrom(o => o.Tags.Select(x => new Tags() { Tag = x })))
                .ForMember(x => x.Contents, s => s.MapFrom(o => o.Content));

            CreateMap<CreateStoryDto, NStory>()
                .ForMember(x => x.Content, s => s.MapFrom(o => o.Content))
                .ForMember(x=>x.Sound,s=>s.MapFrom(o=>o.Sound))
                .ForMember(x => x.TaggedUsers, s => s.MapFrom(o => o.Tagged));

            CreateMap<CreateCommentaryDto, NCommentary>()
                .ForMember(x => x.Post, s => s.MapFrom(o => o.Post));

            CreateMap<NContentObject, SmallContentDto>();

            CreateMap<NContentObject, ContentDto>()
                .ForMember(x => x.LikesCount, s => s.MapFrom(o=>o.Likes.Count))
                .ForMember(x => x.IsLiked, s => s.Ignore());

            CreateMap<NPost, GeoPostDto>()
                .ForMember(x => x.Author, s => s.MapFrom(o => o.Author))
                .ForMember(x => x.Content, s => s.MapFrom(o => o.Contents))
                .ForMember(x => x.LikesCount, s => s.MapFrom(o => o.Likes.Count));
        }

    }
}

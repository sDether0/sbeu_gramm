using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

using SBEU.Gramm.Api.Service;
using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Middleware.Repositories;
using SBEU.Gramm.Middleware.Repositories.Interfaces;
using SBEU.Gramm.Models.Requests.Create;
using SBEU.Gramm.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;

using PostDto = SBEU.Gramm.Models.Responses.PostDto;

namespace SBEU.Gramm.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class PostController : ControllerExt
    {
        private readonly INPostRepository _posts;
        private readonly IMapper _mapper;

        public PostController(IMapper mapper, INPostRepository posts)
        {
            _mapper = mapper;
            _posts = posts;
        }

        [SwaggerOperation("Get all posts")]
        [SwaggerResponse(200, "Return all posts", typeof(List<SmallPostDto>))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return await Try(async () =>
            {
                var posts = await _posts.GetAll(UserId, skip, take);
                var postsDto = posts.ToList().Select(x => _mapper.Map<SmallPostDto>(x));
                postsDto = postsDto.Select(entity =>
                {
                    entity.IsLiked = entity.IsLiked(posts, UserId);
                    return entity;
                });
                return Json(postsDto);
            });
        }

        [SwaggerOperation("Get nearby posts")]
        [SwaggerResponse(200, "Return nearby posts", typeof(List<GeoPostDto>))]
        [HttpGet("radius")]
        public async Task<IActionResult> GePostInRadius([FromQuery] double lat, [FromQuery] double lng)
        {
            return await Try(async () =>
            {
                var posts = await _posts.GetRadius(UserId, lat, lng);
                var postsdto = posts.ToList().Select(x => _mapper.Map<GeoPostDto>(x));
                return Json(postsdto);
            });
        }


        [SwaggerOperation("Get post by id")]
        [SwaggerResponse(200, "Return post by id", typeof(PostDto))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return await Try(async () =>
            {
                var post = await _posts.GetById(id);

                var postDto = _mapper.Map<PostDto>(post);
                postDto.IsLiked = postDto.IsLiked(new[] { post }, UserId);
                return Json(postDto);
            });
        }

        [SwaggerOperation("Get post by id")]
        [SwaggerResponse(200, "Return small post by id", typeof(SmallPostDto))]
        [HttpGet("{id}/small")]
        public async Task<IActionResult> GetByIdSmall(string id)
        {
            return await Try(async () =>
            {
                var post = await _posts.GetById(id);

                var smallPostDto = _mapper.Map<SmallPostDto>(post);
                smallPostDto.IsLiked = smallPostDto.IsLiked(new[] { post }, UserId);
                return Json(smallPostDto);
            });
        }

        [SwaggerOperation("Get tags by post id")]
        [SwaggerResponse(200, "Return tags by post id", typeof(List<string>))]
        [HttpGet("{id}/tags")]
        public async Task<IActionResult> GetTagsByPostId(string id)
        {
            return await Try(async () =>
            {
                var post = await _posts.GetById(id);
                var tags = post.Tags.Select(x=>x.Tag);
                return Json(tags);
            });
        }

        [SwaggerOperation("Get tagged users by post id")]
        [SwaggerResponse(200, "Return tagged users by post id", typeof(List<SmallUserDto>))]
        [HttpGet("{id}/taggedusers")]
        public async Task<IActionResult> GetTaggedUsersByPostId(string id)
        {
            return await Try(async () =>
            {
                var post = await _posts.GetById(id);
                var taggedUsers = post.TaggedUsers.ToList().Select(x=>_mapper.Map<SmallUserDto>(x));
                return Json(taggedUsers);
            });
        }

        [SwaggerOperation("Create post")]
        [SwaggerResponse(200, "Return created post", typeof(PostDto))]
        [SwaggerResponse(441, "Model is null")]
        [SwaggerResponse(442, "Content is null")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto post)
        {
            return await Try(async () =>
            {
                if (post == null) return BadRequest("Model is null", 441);
                if (post.Content == null || post.Content.IsNullOrEmpty()) return BadRequest("Content is null", 442);
                var nPost = _mapper.Map<NPost>(post);
                nPost = await _posts.Create(nPost, UserId);
                var postDto = _mapper.Map<PostDto>(nPost);
                return Json(postDto);
            });
        }
        [SwaggerOperation("Update post")]
        [SwaggerResponse(200, "Return updated post", typeof(PostDto))]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] CreatePostDto post, string id)
        {
            return await Try(async () =>
            {
                var nPost = _mapper.Map<NPost>(post);
                nPost = await _posts.Update(nPost, id, UserId);
                var postDto = _mapper.Map<PostDto>(nPost);
                return Json(postDto);
            });
        }

        [SwaggerOperation("Delete post")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await Try(async () =>
            {
                var result = await _posts.Delete(id);
                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            });
        }

        [SwaggerOperation("Mark post as watched")]
        [HttpPatch("watch/{id}")]
        public async Task<IActionResult> Watch(string id)
        {
            return await Try(async () =>
            {
                await _posts.Watch(id, UserId);
                return Ok();
            });
        }
    }
}

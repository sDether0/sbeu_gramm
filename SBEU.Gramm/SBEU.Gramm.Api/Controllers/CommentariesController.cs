using AutoMapper;
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

namespace SBEU.Gramm.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentariesController : ControllerExt
    {
        private readonly INCommentaryRepository _comments;
        private readonly IMapper _mapper;
        public CommentariesController( IMapper mapper, INCommentaryRepository comments)
        {
            _mapper = mapper;
            _comments = comments;
        }
        
        [SwaggerOperation("Get commentary by post id")]
        [SwaggerResponse(200, "Return commentaries by post", typeof(List<CommentaryDto>))]
        [HttpGet("{postid}")]
        public async Task<IActionResult> GetById(string postid)
        {
            return await Try(async () =>
            {
                var comment = await _comments.GetByPost(postid);
                var commentDto = comment.ToList().Select(x => _mapper.Map<CommentaryDto>(x));
                commentDto = commentDto.Select(entity =>
                {
                    entity.IsLiked = entity.IsLiked(comment, UserId);
                    return entity;
                });
                return Json(commentDto);
            });
        }

        [SwaggerOperation("Create commentary")]
        [SwaggerResponse(200, "Return created commentary", typeof(CommentaryDto))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentaryDto comment)
        {
            return await Try(async () =>
            {
                var nComment = _mapper.Map<NCommentary>(comment);
                nComment = await _comments.Create(nComment, UserId);
                var dto = _mapper.Map<CommentaryDto>(nComment);
                return Json(dto);
            });
        }

        [SwaggerOperation("Delete commentary")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await Try(async () =>
            {
                var result = await _comments.Delete(id);
                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            });
        }

        [SwaggerOperation("Update commentary")]
        [SwaggerResponse(200, "Return updated commentary", typeof(CommentaryDto))]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] CreateCommentaryDto comment, string id)
        {
            return await Try(async () =>
            {
                var nComment = _mapper.Map<NCommentary>(comment);
                nComment = await _comments.Update(nComment, id, UserId);
                var commentDto = _mapper.Map<CommentaryDto>(nComment);
                return Json(commentDto);
            });
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEU.Gramm.Api.Service;
using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.Middleware.Repositories.Interfaces;
using SBEU.Gramm.Models.Responses;

using Swashbuckle.AspNetCore.Annotations;

namespace SBEU.Gramm.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LikesController : ControllerExt
    {
        private readonly INLikeRepository _likes;
        private readonly IMapper _mapper;


        public LikesController( IMapper mapper, INLikeRepository likes)
        {
            _mapper = mapper;
            _likes = likes;
        }

        [SwaggerOperation("Get likes by content")]
        [SwaggerResponse(200, "Return likes by content", typeof(List<LikeDto>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByContentId(string id)
        {
            return await Try(async () =>
            {
                var likes = await _likes.GetByContentId(id);
                var likesDto = likes.Select(x => _mapper.Map<LikeDto>(x));
                return Json(likesDto);
            });
        }

        [SwaggerOperation("Like content")]
        [HttpPost("{id}")]
        public async Task<IActionResult> Create(string id)
        {
            return await Try(async () =>
            {
                await _likes.Create(UserId, id);
                return Ok();
            });
        }

        [SwaggerOperation("Unlike content")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await Try(async () =>
            {
                await _likes.Delete(UserId, id);
                return Ok();
            });
        }
        
    }
}

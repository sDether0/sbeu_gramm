using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBEU.Gramm.Api.Service;
using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.Middleware.Repositories;
using SBEU.Gramm.Models.Responses;

using Swashbuckle.AspNetCore.Annotations;

namespace SBEU.Gramm.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MiscController : ControllerExt
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        public MiscController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [SwaggerOperation("Get all tags")]
        [SwaggerResponse(200, "Return most popular tags", typeof(List<string>))]
        [HttpGet("/tags")]
        public async Task<IActionResult> GetTags([FromQuery] string? filter)
        {
            return await Try(async () =>
            {
                IQueryable<string> tags = null;
                if (filter != null)
                {
                    tags = _context.Tags.OrderByDescending(x => x.Popularity)
                        .Where(x => x.Tag.ToUpper().Contains(filter.ToUpper())).Select(x => x.Tag).Take(100);
                }
                else
                {
                    tags = _context.Tags.OrderByDescending(x => x.Popularity).Select(x => x.Tag).Take(100);
                }

                return Json(tags);
            });
        }


        [SwaggerOperation("Get content by name/id")]
        [SwaggerResponse(200, "Return content", typeof(ContentDto))]
        [HttpGet("/content/{id}")]
        public async Task<IActionResult> GetContent(string id)
        {
            return await Try(async () =>
            {
                var content = await _context.ContentObjects.FindAsync(id);
                if (content == null) return NotFound();
                var contentDto = _mapper.Map<ContentDto>(content);
                contentDto.IsLiked = contentDto.IsLiked(new[] { content }, UserId);
                return Json(contentDto);
            });
        }
    }
}

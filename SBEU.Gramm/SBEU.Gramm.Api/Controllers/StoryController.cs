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
    public class StoryController : ControllerExt
    {
        private readonly INStoryRepository _stories;
        private readonly IMapper _mapper;

        public StoryController(IMapper mapper, INStoryRepository stories)
        {
            _mapper = mapper;
            _stories = stories;
        }

        [SwaggerOperation("Get actual stories")]
        [SwaggerResponse(200, "Return stories", typeof(List<StoryDto>))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await Try(async () =>
            {
                var stories = await _stories.GetAll(UserId);
                var storiesDto = stories.ToList().Select(x => _mapper.Map<StoryDto>(x));
                storiesDto = storiesDto.Select(entity =>
                {
                    entity.IsLiked = entity.IsLiked(stories, UserId);
                    entity.IsWatched = stories.First(x => x.Id == entity.Id).IsWatched(UserId);
                    return entity;
                });
                return Json(storiesDto);
            });
        }

        [SwaggerOperation("Get story by id")]
        [SwaggerResponse(200, "Return story", typeof(StoryDto))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return await Try(async () =>
            {
                var story = await _stories.GetById(id);
                var storyDto = _mapper.Map<StoryDto>(story);
                return Json(storyDto);
            });
        }

        [SwaggerOperation("Create story")]
        [SwaggerResponse(200, "Return created story", typeof(StoryDto))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStoryDto story)
        {
            return await Try(async () =>
            {
                var nStory = _mapper.Map<NStory>(story);
                nStory = await _stories.Create(nStory, UserId);
                var dto = _mapper.Map<StoryDto>(nStory);
                return Json(dto);
            });
        }

        [SwaggerOperation("Update story")]
        [SwaggerResponse(200, "Return updated story", typeof(StoryDto))]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] CreateStoryDto story, string id)
        {
            return await Try(async () =>
            {
                var nStory = _mapper.Map<NStory>(story);
                nStory = await _stories.Update(nStory, id, UserId);
                var dto = _mapper.Map<StoryDto>(nStory);
                return Json(dto);
            });
        }

        [SwaggerOperation("Delete story")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await Try(async () =>
            {
                var result = await _stories.Delete(id);
                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            });
        }

        [SwaggerOperation("Mark story as watched")]
        [HttpPatch("watch/{id}")]
        public async Task<IActionResult> Watch(string id)
        {
            return await Try(async () =>
            {
                await _stories.Watch(id, UserId);
                return Ok();
            });
        }
    }
}

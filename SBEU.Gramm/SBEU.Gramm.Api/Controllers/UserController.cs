using AutoMapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SBEU.Gramm.Api.Service;
using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Middleware.Repositories;
using SBEU.Gramm.Middleware.Repositories.Interfaces;
using SBEU.Gramm.Middleware.SMTP;
using SBEU.Gramm.Models.Requests;
using SBEU.Gramm.Models.Requests.Update;
using SBEU.Gramm.Models.Responses;

using Swashbuckle.AspNetCore.Annotations;

namespace SBEU.Gramm.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class UserController : ControllerExt
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper,  IUserRepository users)
        {
            _mapper = mapper;
            _users = users;
        }

        [SwaggerOperation("Get current user")]
        [SwaggerResponse(200, "Return current user", typeof(UserDto))]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            return await Try(async () =>
            {
                var user = await _users.GetByIdAsync(UserId);
                var userDto = _mapper.Map<UserDto>(user);
                foreach(var post in userDto.Posts){
                    post.IsLiked = post.IsLiked(user.Posts,UserId);
                }
                return Ok(userDto);
            });
        }

        /// <summary>
        /// GetMyName() returns the current user's name
        /// </summary>
        /// <returns>
        /// The current user's username.
        /// </returns>
        [SwaggerOperation("Get current username")]
        [SwaggerResponse(200, "Return current user", typeof(string))]
        [HttpGet("myname")]
        public async Task<IActionResult> GetMyName()
        {
            return await Try(async () =>
            {

                var myName = await _users.GetCurrentUserNameAsync(UserId);
                return Ok(myName);
            });
        }

        [SwaggerOperation("Get all users")]
        [SwaggerResponse(200, "Return all users", typeof(List<SmallUserDto>))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? userName, [FromQuery] int skip = 0, [FromQuery] int take = 25)
        {
            return await Try(async () =>
            {

                var users = await _users.GetAllAsync(userName,skip,take);
                var usersDto = users.ToList().Select(x => _mapper.Map<SmallUserDto>(x));
                return Ok(usersDto);
            });
        }

        [SwaggerOperation("Get user by id")]
        [SwaggerResponse(200, "Return user", typeof(UserDto))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return await Try(async () =>
            {
                var user = await _users.GetByIdAsync(id);
                var userDto = _mapper.Map<UserDto>(user);
                foreach(var post in userDto.Posts){
                    post.IsLiked = post.IsLiked(user.Posts,UserId);
                }
                userDto.IsFollow = user.IsFollow(UserId);
                return Ok(userDto);
            });
        }

        /// <summary>
        /// It takes a user id, finds the user, finds the current user, adds the user to the current
        /// user's following list, updates the current user, and saves the changes
        /// </summary>
        /// <param name="id">The id of the user to follow</param>
        /// <returns>
        /// The return type is IActionResult.
        /// </returns>
        [SwaggerOperation("Follow user")]
        [HttpPost("follow/{id}")]
        public async Task<IActionResult> Follow(string id)
        {
            return await Try(async () =>
            {
                await _users.FollowAsync(UserId, id);
                return Ok();
            });
        }

        [SwaggerOperation("UnFollow user")]
        [HttpPost("unfollow/{id}")]
        public async Task<IActionResult> UnFollow(string id)
        {
            return await Try(async () =>
            {
                await _users.UnFollowAsync(UserId, id);
                return Ok();
            });
        }

        [SwaggerOperation("Update current user")]
        [SwaggerResponse(200, "Return updated user", typeof(UserDto))]
        [HttpPatch]
        public async Task<IActionResult> Edit([FromBody] UpdateUserDto userDto)
        {
            return await Try(async () =>
            {
                var user = await _users.UpdateAsync(UserId, userDto);
                var dto = _mapper.Map<UserDto>(user);
                return Ok(dto);
            });
        }

        [SwaggerOperation("Update email or/and username for current user")]
        [SwaggerResponse(200, "Return confirmation id", typeof(string))]
        [HttpPatch("editroot")]
        public async Task<IActionResult> EditRoot([FromBody] UpdateUserCredDto editDetails)
        {
            return await Try(async () =>
            {
                var id = await _users.UpdateRootAsync(UserId, editDetails);
                return Ok(id);
            });
        }

        [SwaggerOperation("Confirm update email or/and username")]
        [HttpPost("{id}/{code}")]
        public async Task<IActionResult> Confirm(string id, string code)
        {
            return await Try(async () =>
            {
                var result = await _users.ConfirmRootAsync(UserId, id, code);
                if (string.IsNullOrWhiteSpace(result))
                {
                    return Ok();
                }
                return ValidationProblem(result);
            });
        }

        [HttpGet("contents")]
        public async Task<IActionResult> GetContents()
        {
            return await Try(async () =>
            {
                var contents = await _users.GetContentsAsync(UserId);
                var contentsDto = contents.Select(x => _mapper.Map<SmallContentDto>(x));
                return Ok(contentsDto);
            });
        }

        [HttpPost("contents/move/{id}/{position}")]
        public async Task<IActionResult> MoveContent(string id, uint position)
        {
            return await Try(async () =>
            {
                await _users.MoveContent(id, position, UserId);
                return Ok();
            });
        }
    }
}

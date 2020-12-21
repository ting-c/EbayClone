using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EbayClone.API.Resources;
using EbayClone.API.Validators;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EbayClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
                return NotFound();

            var userResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);

            return Ok(userResources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) 
                return NotFound();
            
            var userResource = _mapper.Map<User, UserResource>(user);

            return Ok(userResource);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUser([FromBody] SaveUserResource saveUserResource)
        {
            var validator = new SaveUserResourceValidator();
            ValidationResult results = await validator.ValidateAsync(saveUserResource);

            if (!results.IsValid)
                return BadRequest(results.Errors);

            User userToCreate = _mapper.Map<SaveUserResource, User>(saveUserResource);

            var newUser = await _userService.CreateUser(userToCreate); 

            var user = await _userService.GetUserById(newUser.Id);

            UserResource userResource = _mapper.Map<User, UserResource>(user);

            return Ok(userResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] SaveUserResource saveUserResource)
        {
			var validator = new SaveUserResourceValidator();
			ValidationResult results = await validator.ValidateAsync(saveUserResource);

			if (!results.IsValid)
				return BadRequest(results.Errors);

			var userToBeUpdated = await _userService.GetUserById(id);

			if (userToBeUpdated != null)
				return NotFound();

			User user = _mapper.Map<SaveUserResource, User>(saveUserResource);

			await _userService.UpdateUser(userToBeUpdated, user);

			User updatedUser = await _userService.GetUserById(id);

			UserResource updatedUserResource = _mapper.Map<User, UserResource>(updatedUser);

			return Ok(updatedUserResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
                return NotFound();

            await _userService.DeleteUser(user);

            return NoContent();
        }
    }
}
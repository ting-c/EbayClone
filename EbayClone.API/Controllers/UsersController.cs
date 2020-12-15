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
        public async Task<ActionResult<IEnumerable<UserResource>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            var userResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);

            return Ok(userResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResource>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            var userResource = _mapper.Map<User, UserResource>(user);

            return Ok(userResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<UserResource>> CreateUser([FromBody] SaveUserResource saveUserResource)
        {
            var validator = new SaveUserResourceValidator();
            ValidationResult results = await validator.ValidateAsync(saveUserResource);

            if (results.IsValid)
                return BadRequest(results.Errors);

            User userToCreate = _mapper.Map<SaveUserResource, User>(saveUserResource);

            var newUser = await _userService.CreateUser(userToCreate); 

            var user = await _userService.GetUserById(newUser.Id);

            UserResource userResource = _mapper.Map<User, UserResource>(user);

            return Ok(userResource);
        }
    }
}
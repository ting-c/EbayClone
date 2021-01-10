using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EbayClone.API.Resources;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EbayClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(IMapper mapper, IAuthService authService)
        {
            this._mapper = mapper;
            this._authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource, string roleName="client")
        {
            if (await _authService.FindUserByEmail(userSignUpResource.Email) != null)
                return Conflict("Email has already been taken");

            if (await _authService.FindUserByUsername(userSignUpResource.UserName) != null)
                return Conflict("Username has already been taken");
                
            var user = _mapper.Map<UserSignUpResource, User>(userSignUpResource);
            bool IsSuccess = await _authService.CreateNewUser(user, userSignUpResource.Password, roleName);
            
            if (!IsSuccess)
                return Problem("Sign up error", null, 500);

            return Created(string.Empty, "Sign up success");
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserSignInResource userSignInResource)
        {
            var user = await _authService.FindUserByEmail(userSignInResource.Email);

            if (user == null)
                return NotFound("User not found");

            var isCorrect = await _authService.IsUserPasswordCorrect(user, userSignInResource.Password);

            if (!isCorrect)
                return BadRequest("Email or password is incorrect");
            
            var roles = await _authService.GetUserRoles(user);

            var jwtString = _authService.GenerateJwt(user, roles);

            return Ok(jwtString);
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateNewRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Role name must be provided");
            
;           var result = await _authService.CreateNewRole(roleName);

            if (!result.Succeeded)
                return Problem(result.Errors.First().Description, null, 500);

            return Ok();
        }

        [HttpPost("user/{userEmail}/role")]
        public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        {
            var user = await _authService.FindUserByEmail(userEmail);
            
            if (user == null)
                return NotFound("User not found");
                
            var result = await _authService.AddUserToRole(user, roleName);

            if (!result.Succeeded)
                return Problem(result.Errors.First().Description, null, 500);

            return Ok();
        }
    }
}
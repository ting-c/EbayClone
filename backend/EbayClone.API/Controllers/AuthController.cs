using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EbayClone.API.Resources;
using EbayClone.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EbayClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthController(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource)
        {
            var result = await IsCreateUserSuccess(userSignUpResource);

            if (!result.Succeeded)
                // return first error description with 500 status code
                return Problem(result.Errors.First().Description, null, 500);

            // return empty CreatedResult object
            return Created(string.Empty, null);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserSignInResource userSignInResource)
        {
            var user = FindUserByEmail(userSignInResource.Email);

            if (user == null)
                return NotFound("User not found");

            var isCorrect = await IsUserPasswordCorrect(user, userSignInResource.Password);

            if (!isCorrect)
                return BadRequest("Email or password is incorrect");

            return Ok();
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Role name must be provided");
            
;           var result = await CreateNewRole(roleName);

            if (!result.Succeeded)
                return Problem(result.Errors.First().Description, null, 500);

            return Ok();
        }

        [HttpPost("user/{userEmail}/role")]
        public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        {
            var user = FindUserByEmail(userEmail);
            
            if (user == null)
                return NotFound("User not found");
                
            var result = await AddToRole(user, roleName);

            if (!result.Succeeded)
                return Problem(result.Errors.First().Description, null, 500);

            return Ok();
        }

        private async Task<IdentityResult> CreateNewRole(string roleName)
        {
            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            return roleResult;
        }

        private async Task<IdentityResult> AddToRole(User user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        private User FindUserByEmail(string email)
        {
            return _userManager.Users.SingleOrDefault(u => u.Email == email);
        }

        private async Task<bool> IsUserPasswordCorrect(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        private async Task<IdentityResult> IsCreateUserSuccess(UserSignUpResource userSignUpResource)
        {
			var user = _mapper.Map<UserSignUpResource, User>(userSignUpResource);

			var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);

            return userCreateResult;
		}
    }
}
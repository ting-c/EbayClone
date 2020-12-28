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

        public AuthController(IMapper mapper, UserManager<User> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource)
        {
            var user = _mapper.Map<UserSignUpResource, User>(userSignUpResource);

            var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);

            if (!userCreateResult.Succeeded)
                // return first error description with 500 status code
                return Problem(userCreateResult.Errors.First().Description, null, 500);

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

        private User FindUserByEmail(string email)
        {
            return _userManager.Users.SingleOrDefault(u => u.Email == email);
        }
        private async Task<bool> IsUserPasswordCorrect(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
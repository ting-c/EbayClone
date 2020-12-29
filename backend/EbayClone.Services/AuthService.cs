using System.Threading.Tasks;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace EbayClone.Services
{
    public class AuthService : IAuthService
    {
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			this._userManager = userManager;
			this._roleManager = roleManager;
		}

		public async Task<IdentityResult> AddUserToRole(User user, string roleName)
		{
			var result = await _userManager.AddToRoleAsync(user, roleName);
			return result;
		}

		public async Task<IdentityResult> CreateNewRole(string roleName)
		{
			var newRole = new Role
			{
				Name = roleName
			};
			var result = await _roleManager.CreateAsync(newRole);
			return result;
		}

		public async Task<IdentityResult> CreateNewUser(User user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);
			return result;
		}

		public async Task<User> FindUserByEmail(string email)
		{
			var result = await _userManager.FindByEmailAsync(email);
			return result;
		}

		public async Task<bool> IsUserPasswordCorrect(User user, string password)
		{
			var result = await _userManager.CheckPasswordAsync(user, password);
			return result;
		}
	}
}
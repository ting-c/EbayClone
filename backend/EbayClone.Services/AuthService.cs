using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EbayClone.API.Settings;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EbayClone.Services
{
    public class AuthService : IAuthService
    {
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly JwtSettings _jwtSettings;

		public AuthService(
			UserManager<User> userManager, 
			RoleManager<Role> roleManager, 
			IOptionsSnapshot<JwtSettings> jwtSettings)
		{
			this._userManager = userManager;
			this._roleManager = roleManager;
			this._jwtSettings = jwtSettings.Value;
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

		public async Task<IList<string>> GetUserRoles(User user)
		{
			var roles = await _userManager.GetRolesAsync(user);
			return roles;
		}

		public string GenerateJwt(User user, IList<string> roles)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
			claims.AddRange(roleClaims);

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Issuer,
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
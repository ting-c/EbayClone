using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace EbayClone.Core.Services
{
    public interface IAuthService
    {
        Task<bool> CreateNewUser(User user, string password, string roleName);
        Task<User> FindUserByEmail(string email);
        Task<User> FindUserByUsername(string userName);
        Task<bool> IsRoleExists(string roleName);
        Task<bool> IsUserPasswordCorrect(User user, string password);
        Task<IdentityResult> CreateNewRole(string roleName);
        Task<IdentityResult> AddUserToRole(User user, string roleName);
        Task<IList<string>> GetUserRoles(User user);
		string GenerateJwt(User user, IList<string> roles);
    }
}
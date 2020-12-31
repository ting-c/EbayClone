using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace EbayClone.Core.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> CreateNewUser(User user, string password);
        Task<User> FindUserByEmail(string email);
        Task<User> FindUserByUsername(string userName);
        Task<bool> IsUserPasswordCorrect(User user, string password);
        Task<IdentityResult> CreateNewRole(string roleName);
        Task<IdentityResult> AddUserToRole(User user, string roleName);
        Task<IList<string>> GetUserRoles(User user);
		string GenerateJwt(User user, IList<string> roles);
    }
}
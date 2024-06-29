using Ecommerce.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Api.Extensions
{
    public static class UserManagerExtenstions
    {
        public static async Task<AppUser> FindUserByClaimsPrincipleWithAddress(this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            return await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        }
        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
            => await userManager.Users.SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        

    }
}

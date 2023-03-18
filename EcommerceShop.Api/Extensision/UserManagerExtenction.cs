using EcommerceShop.Core.Model.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace EcommerceShop.Api.Extensision
{
    public static class UserManagerExtenction
    {
        public static async Task<AppUser> FindByEmailWithAddress(
            this UserManager<AppUser> input,ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            // var email = user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(y => y.Email == email);
        }

        public static async Task<AppUser> FindByEmailClaimsPrincipal(this UserManager<AppUser> input, ClaimsPrincipal user)
        {


            var email = user.FindFirstValue(ClaimTypes.Email);
            // var email = user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync(y => y.Email == email);
        }
    }
}

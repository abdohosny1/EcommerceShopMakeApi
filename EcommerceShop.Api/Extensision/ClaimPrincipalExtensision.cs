using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace EcommerceShop.Api.Extensision
{
    public static class ClaimPrincipalExtensision
    {

        public static string  RetrivewEmailFromPrincipal(this ClaimsPrincipal user)
        {
            //User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?
             //    .Value;
            return user.FindFirstValue(ClaimTypes.Email);
        }
         
    }
}

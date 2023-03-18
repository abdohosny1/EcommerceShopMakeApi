using EcommerceShop.Core.Model.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Repository
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
     
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Repository
{
    public interface IMailSendService
    {
        Task SendingEmail(string mailTo, string subject, string body);//, IList<IFormFile> ATTACHMENT = null);

    }
}

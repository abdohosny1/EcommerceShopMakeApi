using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Model.OrderAggragate
{
    public class Address
    {
        public Address() { }
        public Address(string firstName, string lasttName, string streat, string city, string state, string zipCode)
        {
            FirstName = firstName;
            LasttName = lasttName;
            Streat = streat;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public string Streat { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }
}

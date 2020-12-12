using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Api.DTO
{
    public class AddressDTO
    {

        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
    }
}

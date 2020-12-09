using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Contract.Models
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
    }
}
